using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Params;
using Application.Resources.Bills.Get;
using Application.Resources.Bills.Save;
using Application.Responses;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;
        public BillService(IBillRepository billRepository, IOrderedCourseRepository orderedCourseRepository, IStatusesRepository statusesRepository, IStatusEntriesRepository statusEntriesRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
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

        public async Task<Response<PagedList<Bill>>> ListAsync(PagingParams queryParams)
        {
            var bills = await billRepository.ListAsync();

            var pagedList = PagedList<Bill>.ToPagedList(bills, queryParams.PageNumber, queryParams.PageSize);

            return new Response<PagedList<Bill>>(HttpStatusCode.OK, pagedList);
        }
        public async Task<Response<Bill>> SaveAsync(SaveBillResource bill)
        {
            if (bill.CustomerId != null)
            {
                var existingCustomer = await customerRepository.GetAsync(bill.CustomerId.GetValueOrDefault());
                if (existingCustomer == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"Customer with id:{bill.CustomerId} not found");
                }
            }

            var existingOrder = await orderRepository.GetOrderAsync(bill.OrderId);

            var paidStatusForBill = await statusesRepository.GetStatusByNameAsync(Domain.Enums.StatusNameEnum.paid.ToString());

            var newStatusEntry = new StatusEntry
            {
                Id = Guid.NewGuid(),
                Note = String.Empty,
                StatusId = paidStatusForBill.Id,
                OrderId = existingOrder.Id,
                Created = DateTime.Now
            };

            await statusEntriesRepository.SaveAsync(newStatusEntry);


            if (existingOrder == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Order with id:{bill.OrderId} not found");
            }

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
                if (existingOrderedCourse == null)
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

            if (existingBill == null)
            {
                return new Response<Bill>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            Customer existingCustomer = null;

            if (bill.CustomerId != null)
            {
                existingCustomer = await customerRepository.GetAsync(bill.CustomerId.GetValueOrDefault());
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

            foreach (var course in coursesForExistingBill)
            {
                course.BillId = null;
                course.BillQuantity = null;
                orderedCourseRepository.Update(course);
            }

            double netPrice = 0;

            foreach (var course in bill.OrderedCourses)
            {
                var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(course.Id);
                if (existingOrderedCourse == null)
                {
                    return new Response<Bill>(HttpStatusCode.NotFound, $"OrderedCourse with id:{course.Id} not found");
                }

                existingOrderedCourse.Bill = existingBill;
                existingOrderedCourse.BillQuantity = course.Quantity;
                netPrice += (existingOrderedCourse.Course.NetPrice * course.Quantity);
                orderedCourseRepository.Update(existingOrderedCourse);
            }

            existingBill.Customer = existingCustomer;
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

            foreach (var course in coursesForExistingBill)
            {
                course.BillQuantity = null;
                orderedCourseRepository.Update(course);
            }

            billRepository.Delete(existingBill);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Bill>(HttpStatusCode.NoContent, existingBill);
        }

        public async Task<Response<IEnumerable<OrderedCourse>>> GetOrderedCourseForBillAsync(Guid id)
        {
            var existingBill = await billRepository.GetBillAsync(id);

            if (existingBill == null)
            {
                return new Response<IEnumerable<OrderedCourse>>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            var courses = await orderedCourseRepository.GetCoursesForBillId(id);

            return new Response<IEnumerable<OrderedCourse>>(HttpStatusCode.OK, courses);
        }
    }
}