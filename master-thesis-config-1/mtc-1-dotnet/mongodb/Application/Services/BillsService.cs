using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.Bills.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository billRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderedCourseRepository orderedCourseRepository;
        private readonly IStatusEntriesRepository statusEntriesRepository;
        private readonly IStatusesRepository statusesRepository;
        public BillService(IBillRepository billRepository, IOrderedCourseRepository orderedCourseRepository, IStatusesRepository statusesRepository, IStatusEntriesRepository statusEntriesRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            this.orderedCourseRepository = orderedCourseRepository;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
            this.billRepository = billRepository;
            this.statusEntriesRepository = statusEntriesRepository;
            this.statusesRepository = statusesRepository;
        }

        public async Task<Response<Bill>> GetBillAsync(Guid id)
        {
            var existingBill = await billRepository.GetBillAsync(id);

            if (existingBill == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            return new Response<Bill>(HttpStatusCode.OK, existingBill);
        }

        public async Task<Response<List<Bill>>> ListAsync()
        {
            var bills = await billRepository.ListAsync();
            return new Response<List<Bill>>(HttpStatusCode.OK, bills);
        }
        public async Task<Response<Bill>> SaveAsync(SaveBillResource bill)
        {
            if (bill.MongoCustomerId != null)
            {
                var existingCustomer = await customerRepository.GetAsync(bill.MongoCustomerId);
                if (existingCustomer == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"Customer with id:{bill.CustomerId} not found");
                }
            }

            var existingOrder = await orderRepository.GetOrderAsync(bill.OrderId);
            
            if (existingOrder == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Order with id:{bill.OrderId} not found");
            }

            var paidStatusForBill = await statusesRepository.GetStatusByNameAsync(Domain.Enums.StatusNameEnum.paid.ToString());

            var newStatusEntry = new StatusEntry
            {
                Id = Guid.NewGuid(),
                Note = string.Empty,
                StatusId = paidStatusForBill.Id,
                OrderId = existingOrder.Id,
                Created = DateTime.Now
            };

            await statusEntriesRepository.SaveAsync(newStatusEntry);

            var savedBillId = Guid.NewGuid();

            var savedBill = new Bill
            {
                Id = savedBillId,
                CustomerId = bill.CustomerId,
                OrderId = bill.OrderId,
                Created = DateTime.Now,
                Tax = bill.Tax
            };

            double netPrice = 0;

            foreach (var orderedCourse in bill.OrderedCourses)
            {
                var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(orderedCourse.Id);
                if(existingOrderedCourse == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"OrderedCourse with id:{orderedCourse.Id} not found");
                }

                existingOrderedCourse.Bill = savedBill;
                existingOrderedCourse.BillQuantity = orderedCourse.Quantity;
                netPrice += (existingOrderedCourse.Course.NetPrice * orderedCourse.Quantity);
                orderedCourseRepository.Update(existingOrderedCourse);
            }

            savedBill.NetPrice = netPrice;

            await billRepository.SaveAsync(savedBill);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Bill>(HttpStatusCode.NoContent, savedBill);
        }

        public async Task<Response<Bill>> UpdateAsync(Guid id, SaveBillResource bill)
        {
            var existingBill = await billRepository.GetBillAsync(id);

            if(existingBill == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            Domain.Models.MongoDb.Customer existingCustomer = null;

            if (bill.MongoCustomerId != null)
            {
                existingCustomer = await customerRepository.GetAsync(bill.MongoCustomerId);
                if (existingCustomer == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"Customer with id:{bill.CustomerId} not found");
                }
            }

            var existingOrder = await orderRepository.GetOrderAsync(bill.OrderId);

            if (existingOrder == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Order with id:{bill.OrderId} not found");
            }

            var coursesForExistingBill = await orderedCourseRepository.GetCoursesForBillId(id);

            foreach(var course in coursesForExistingBill)
            {
                course.BillId = null;
                course.BillQuantity = null;
                orderedCourseRepository.Update(course);
            }

            double netPrice = 0;

            foreach(var course in bill.OrderedCourses)
            {
                var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(course.Id);
                if(existingOrderedCourse == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"OrderedCourse with id:{course.Id} not found");
                }

                existingOrderedCourse.Bill = existingBill;
                existingOrderedCourse.BillQuantity = course.Quantity;
                netPrice += (existingOrderedCourse.Course.NetPrice * course.Quantity);
                orderedCourseRepository.Update(existingOrderedCourse);
            }

            existingBill.MongoCustomer = existingCustomer;
            existingBill.Order = existingOrder;
            existingBill.Tax = bill.Tax;
            existingBill.NetPrice = netPrice;

            billRepository.Update(existingBill);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Bill>(HttpStatusCode.NoContent, existingBill);
        }

        public async Task<Response<Bill>> DeleteAsync(Guid id)
        {
            var existingBill = await billRepository.GetBillAsync(id);

            if (existingBill == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            var coursesForExistingBill = await orderedCourseRepository.GetCoursesForBillId(id);

            foreach(var course in coursesForExistingBill)
            {
                course.BillQuantity = null;
                orderedCourseRepository.Update(course);
            }

            billRepository.Delete(existingBill);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Bill>(HttpStatusCode.NoContent, existingBill);
        }

        public async Task<Response<List<OrderedCourse>>> GetOrderedCourseForBillAsync(Guid id)
        {
            var existingBill = await billRepository.GetBillAsync(id);

            if (existingBill == null)
            {
                return new Response<List<OrderedCourse>>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            var courses = await orderedCourseRepository.GetCoursesForBillId(id);

            return new Response<List<OrderedCourse>>(HttpStatusCode.OK, courses);
        }
    }
}