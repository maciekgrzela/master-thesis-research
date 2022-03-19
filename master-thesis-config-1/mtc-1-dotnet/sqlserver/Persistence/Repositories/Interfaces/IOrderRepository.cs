using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(Guid id);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<List<Order>> ListAsync();
        Task SaveAsync(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}