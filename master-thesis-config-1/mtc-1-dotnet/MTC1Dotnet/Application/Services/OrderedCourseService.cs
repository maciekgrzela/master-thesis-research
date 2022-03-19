using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderedCourseService : IOrderedCourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderedCourseRepository orderedCourseRepository;
        private readonly IStatusesRepository statusesRepository;
        private readonly IStatusEntriesRepository statusEntriesRepository;
        private readonly ICoursesRepository courseRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IBillRepository billRepository;

        public OrderedCourseService(IOrderedCourseRepository orderedCourseRepository,IStatusesRepository  statusesRepository, IStatusEntriesRepository statusEntriesRepository, ICoursesRepository courseRepository, IOrderRepository orderRepository, IBillRepository billRepository, IUnitOfWork unitOfWork)
        {
            this.billRepository = billRepository;
            this.orderRepository = orderRepository;
            this.courseRepository = courseRepository;
            this.orderedCourseRepository = orderedCourseRepository;
            this.unitOfWork = unitOfWork;
            this.statusesRepository = statusesRepository;
            this.statusEntriesRepository = statusEntriesRepository;
        }

        public async Task<Response<OrderedCourse>> DeleteAsync(Guid id)
        {
            var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(id);
            if (existingOrderedCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Ordered course with id:{id} not found");
            }

            orderedCourseRepository.Delete(existingOrderedCourse);
            await unitOfWork.CommitTransactionAsync();

            return new Response<OrderedCourse>(HttpStatusCode.NoContent, existingOrderedCourse);
        }

        public async Task<Response<OrderedCourse>> GetOrderedCourseAsync(Guid id)
        {
            var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(id);

            if (existingOrderedCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Ordered course with id:{id} not found");
            }

            return new Response<OrderedCourse>(HttpStatusCode.OK, existingOrderedCourse);
        }

        public async Task<Response<List<OrderedCourse>>> ListAsync()
        {
            var orderedCourses = await orderedCourseRepository.ListAsync();
            return new Response<List<OrderedCourse>>(HttpStatusCode.OK, orderedCourses);
        }

        public async Task<Response<OrderedCourse>> ModifyStatusAsync(OrderedCourse orderedCourse, StatusNameEnum status, string note)
        {
            var statusForOrderedCourse = await statusesRepository.GetStatusByNameAsync(status.ToString());

            if(statusForOrderedCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Status with name:{status.ToString()} not found!");
            }

            var newStatusEntry = new StatusEntry
            {
                Id = Guid.NewGuid(),
                Status = statusForOrderedCourse,
                OrderedCourse = orderedCourse,
                Created = DateTime.Now,
                Note = note == null ? String.Empty : note
            };

            await statusEntriesRepository.SaveAsync(newStatusEntry);
            await unitOfWork.CommitTransactionAsync();

            return new Response<OrderedCourse>(HttpStatusCode.OK, orderedCourse);
        }

        public async Task<Response<OrderedCourse>> SaveAsync(List<OrderedCourse> orderedCourses)
        {

            foreach (var orderedCourse in orderedCourses)
            {
                var existingCourse = await courseRepository.GetCourseAsync(orderedCourse.CourseId);
                if (existingCourse == null)
                {
                    return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Course with id:{orderedCourse.CourseId} not found");
                }

                var existingOrder = await orderRepository.GetOrderAsync(orderedCourse.OrderId);
                if (existingOrder == null)
                {
                    return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Order with id:{orderedCourse.OrderId} not found");
                }

                Bill existingBill = null;

                var savedOrderedCourse = new OrderedCourse
                {
                    Id = Guid.NewGuid(),
                    Course = existingCourse,
                    Order = existingOrder,
                    PercentageDiscount = orderedCourse.PercentageDiscount,
                    Quantity = orderedCourse.Quantity,
                    BillQuantity = null,
                    StatusEntries = new List<StatusEntry>()
                };

                if (orderedCourse.BillId != null)
                {
                    existingBill = await billRepository.GetBillAsync(orderedCourse.BillId.GetValueOrDefault());
                    if (existingOrder == null)
                    {
                        return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Bill with id:{orderedCourse.BillId} not found");
                    }

                    savedOrderedCourse.Bill = existingBill;
                    savedOrderedCourse.BillQuantity = orderedCourse.BillQuantity;
                }

                

                await orderedCourseRepository.SaveAsync(savedOrderedCourse);
            }

            await unitOfWork.CommitTransactionAsync();

            return new Response<OrderedCourse>(HttpStatusCode.NoContent, new OrderedCourse());
        }

        public async Task<Response<OrderedCourse>> UpdateAsync(Guid id, OrderedCourse orderedCourse)
        {
            var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(id);
            if(existingOrderedCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Ordered Course with id:{id} not found");
            }

            var existingCourse = await courseRepository.GetCourseAsync(orderedCourse.CourseId);
            if (existingCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Course with id:{orderedCourse.CourseId} not found");
            }

            var existingOrder = await orderRepository.GetOrderAsync(orderedCourse.OrderId);
            if (existingOrder == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Order with id:{orderedCourse.OrderId} not found");
            }

            Bill existingBill = null;

            if (orderedCourse.BillId != null)
            {
                existingBill = await billRepository.GetBillAsync(orderedCourse.BillId.GetValueOrDefault());
                if (existingOrder == null)
                {
                    return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Bill with id:{orderedCourse.BillId} not found");
                }

                existingOrderedCourse.Bill = existingBill;
                existingOrderedCourse.BillQuantity = orderedCourse.BillQuantity;
            }


            
            existingOrderedCourse.Course = existingCourse;
            existingOrderedCourse.Order = existingOrder;
            existingOrderedCourse.PercentageDiscount = orderedCourse.PercentageDiscount;
            existingOrderedCourse.Quantity = orderedCourse.Quantity;

            orderedCourseRepository.Update(existingOrderedCourse);
            await unitOfWork.CommitTransactionAsync();

            return new Response<OrderedCourse>(HttpStatusCode.NoContent, existingOrderedCourse);
        }

        public async Task<Response<OrderedCourse>> UpdateStatusAsync(Guid orderedCourseId, string statusName)
        {
            var existingOrderedCourse = await orderedCourseRepository.GetOrderedCourseAsync(orderedCourseId);
            if(existingOrderedCourse == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Ordered Course with id:{orderedCourseId} not found");
            }

            var existingOrder = await orderRepository.GetOrderAsync(existingOrderedCourse.OrderId);
            if (existingOrder == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Order with id:{existingOrderedCourse.OrderId} not found");
            }


            // Domain.Enums.StatusNameEnum.ordered.ToString()
            var orderedStatusForOrder = await statusesRepository.GetStatusByNameAsync(statusName.ToLower());

            if (orderedStatusForOrder == null)
            {
                return new Response<OrderedCourse>(HttpStatusCode.NotFound, $"Status with name:{statusName.ToLower()} not found");
            }

            var statusEntry = new StatusEntry() {
                Id = Guid.NewGuid(),
                StatusId = orderedStatusForOrder.Id,
                Note = string.Empty,
                OrderId = existingOrder.Id,
                OrderedCourseId = existingOrderedCourse.Id,
                Created = DateTime.UtcNow
            };
            
            await statusEntriesRepository.SaveAsync(statusEntry);
            existingOrder.StatusEntries.Add(statusEntry);

            orderRepository.Update(existingOrder);
            await unitOfWork.CommitTransactionAsync();

            return new Response<OrderedCourse>(HttpStatusCode.NoContent, existingOrderedCourse);
        }

    }
}