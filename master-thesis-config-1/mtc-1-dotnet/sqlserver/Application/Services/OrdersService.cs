using Application.Resources.Orders.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderRepository orderRepository;
        private readonly IStatusesRepository statusesRepository;
        private readonly IStatusEntriesRepository statusEntriesRepository;
        private readonly IOrderedCourseRepository orderedCourseRepository;
        private readonly ITableRepository tableRepository;
        private readonly IUserAccessor userAccessor;
        private readonly UserManager<User> userManager;

        public OrdersService(IOrderRepository orderRepository, IStatusEntriesRepository statusEntriesRepository, IOrderedCourseRepository orderedCourseRepository, IStatusesRepository statusesRepository, ITableRepository tableRepository, IUserAccessor userAccessor, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.userAccessor = userAccessor;
            this.orderRepository = orderRepository;
            this.tableRepository = tableRepository;
            this.unitOfWork = unitOfWork;
            this.orderedCourseRepository = orderedCourseRepository;
            this.statusesRepository = statusesRepository;
            this.statusEntriesRepository = statusEntriesRepository;
        }

        public async Task<Response<Order>> GetLastTableOrderAsync(Guid id)
        {
            var table = await tableRepository.GetAsync(id);
            if (table == null)
            {
                return new Response<Order>(HttpStatusCode.NotFound, $"Table with id:{id} not found");
            }

            Order result = null;
            foreach(var order in table.Orders)
            {
                var entryPaid = order.StatusEntries.Where(se => se.Status.Name == "Paid").FirstOrDefault();

                if(entryPaid == null)
                    result = order;
            }

            return new Response<Order>(HttpStatusCode.OK, result);
        }

        public async Task<Response<Order>> GetOrderAsync(Guid id)
        {
            var order = await orderRepository.GetOrderAsync(id);

            if (order == null)
            {
                return new Response<Order>(HttpStatusCode.NotFound, $"Order with id:{id} not found");
            }

            return new Response<Order>(HttpStatusCode.OK, order);
        }

        public async Task<Response<List<Order>>> GetTableOrdersAsync(Guid id)
        {
            var table = await tableRepository.GetAsync(id);
            if (table == null)
            {
                return new Response<List<Order>>(HttpStatusCode.NotFound, $"Table with id:{id} not found");
            }

            return new Response<List<Order>>(HttpStatusCode.OK, table.Orders);
        }

        public async Task<Response<List<Order>>> GetUserOrdersAsync(string id)
        {
            var orders = await orderRepository.GetUserOrdersAsync(id);
            return new Response<List<Order>>(HttpStatusCode.OK, orders);
        }

        public async Task<Response<List<Order>>> ListAsync()
        {
            var orders = await orderRepository.ListAsync();
            return new Response<List<Order>>(HttpStatusCode.OK, orders);
        }

        public async Task<Response<Order>> ModifyStatusAsync(Order order, StatusNameEnum status, string note)
        {
            var statusForOrder = await statusesRepository.GetStatusByNameAsync(status.ToString());

            if(statusForOrder == null)
            {
                return new Response<Order>(HttpStatusCode.NotFound, $"Status with name:{status.ToString()} not found!");
            }

            var newStatusEntry = new StatusEntry
            {
                Id = Guid.NewGuid(),
                Status = statusForOrder,
                Order = order,
                Created = DateTime.Now,
                Note = note == null ? String.Empty : note
            };

            await statusEntriesRepository.SaveAsync(newStatusEntry);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Order>(HttpStatusCode.OK, order);
        }

        public async Task<Response<Order>> SaveAsync(Order order)
        {
            var existingTable = await tableRepository.GetAsync(order.TableId);

            if (existingTable == null)
            {
                return new Response<Order>(HttpStatusCode.NotFound, $"Table with id:{order.TableId} not found");
            }

            var existingUser = await userManager.Users
                .SingleOrDefaultAsync(w => w.UserName == userAccessor.GetLoggedUserName());


            if (existingUser == null)
            {
                return new Response<Order>(HttpStatusCode.Unauthorized, "There is no currently logged user");
            }

            var orderedStatusForOrder = await statusesRepository.GetStatusByNameAsync(Domain.Enums.StatusNameEnum.ordered.ToString());
            var createdStatusForOrderedCourse = await statusesRepository.GetStatusByNameAsync(Domain.Enums.StatusNameEnum.ordered.ToString());


            var newOrderId = Guid.NewGuid();

            var newOrder = new Order()
            {
                Id = newOrderId,
                TableId = order.TableId,
                Note = order.Note,
                OrderedCourses = new List<OrderedCourse>(),
                Bills = new List<Bill>(),
                StatusEntries = new List<StatusEntry>(),
                User = existingUser
            };

            await orderRepository.SaveAsync(newOrder);
            await unitOfWork.CommitTransactionAsync();

            var statusEntryForOrder = new StatusEntry
            {
                Id = Guid.NewGuid(),
                Note = String.Empty,
                Status = orderedStatusForOrder,
                OrderId = newOrder.Id,
                Created = DateTime.Now
            };

            await statusEntriesRepository.SaveAsync(statusEntryForOrder);
            await unitOfWork.CommitTransactionAsync();

            foreach (var orderedCourse in order.OrderedCourses)
            {
                var ordered = new OrderedCourse
                {
                    Id = Guid.NewGuid(),
                    BillId = null,
                    CourseId = orderedCourse.CourseId,
                    OrderId = newOrderId,
                    Quantity = orderedCourse.Quantity,
                    BillQuantity = 0,
                    PercentageDiscount = orderedCourse.PercentageDiscount
                };

                await orderedCourseRepository.SaveAsync(ordered);

                var statusEntryForOrderedCourse = new StatusEntry()
                {
                    Id = Guid.NewGuid(),
                    Note = String.Empty,
                    Status = createdStatusForOrderedCourse,
                    OrderedCourseId = ordered.Id,
                    Created = DateTime.Now
                };

                await statusEntriesRepository.SaveAsync(statusEntryForOrderedCourse);
            }
            await unitOfWork.CommitTransactionAsync();

            return new Response<Order>(HttpStatusCode.NoContent, newOrder);
        }

        public async Task<Response<Order>> UpdateAsync(Guid id, SaveOrderResource order)
        {
            var existingOrder = await orderRepository.GetOrderAsync(id);

            if (existingOrder == null)
            {
                return new Response<Order>(HttpStatusCode.NotFound, $"Order with id:{id} not found");
            }

            existingOrder.TableId = order.TableId;
            existingOrder.Note = order.Note;

            orderRepository.Update(existingOrder);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Order>(HttpStatusCode.NoContent, existingOrder);
        }
    }
}
