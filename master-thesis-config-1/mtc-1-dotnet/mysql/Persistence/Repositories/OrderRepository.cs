using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context) {}

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await context.Orders
                .Include(p => p.Table)
                .Include(p => p.User)
                .Include(p => p.StatusEntries)
                .ThenInclude(p => p.Status)
                .Include(p => p.OrderedCourses)
                .ThenInclude(p => p.Course)
                .Include(p => p.OrderedCourses)
                .ThenInclude(p => p.StatusEntries)
                .ThenInclude(p => p.Status)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await context.Orders
                .Include(p => p.Table)
                .Include(p => p.User)
                .Include(p => p.StatusEntries)
                .ThenInclude(p => p.Status)
                .Include(p => p.OrderedCourses)
                .ThenInclude(p => p.Course)
                .AsSplitQuery()
                .Where(p => p.User.Id == userId).ToListAsync();
        }

        public async Task<List<Order>> ListAsync()
        {
            return await context.Orders
                .Include(p => p.Table)
                .Include(p => p.User)
                .Include(p => p.StatusEntries)
                .ThenInclude(p => p.Status)
                .Include(p => p.OrderedCourses)
                .ThenInclude(p => p.Course)
                .Include(p => p.OrderedCourses)
                .ThenInclude(p => p.StatusEntries)
                .ThenInclude(p => p.Status)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task SaveAsync(Order order)
        {
            await context.Orders.AddAsync(order);
        }

        public void Update(Order order)
        {
            context.Orders.Update(order);
        }

        public void Delete(Order order)
        {
            context.Orders.Remove(order);
        }
    }
}