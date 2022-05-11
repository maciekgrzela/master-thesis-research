using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources.Orders.Save;
using Application.Responses;
using Domain.Enums;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<Response<IEnumerable<Order>>> ListAsync();

        Task<Response<Order>> GetOrderAsync(Guid id);
        Task<Response<Order>> ModifyStatusAsync(Order order, StatusNameEnum status, string note);

        Task<Response<Order>> GetLastTableOrderAsync(Guid id);

        Task<Response<IEnumerable<Order>>> GetTableOrdersAsync(Guid id);

        Task<Response<IEnumerable<Order>>> GetUserOrdersAsync(string id);

        Task<Response<Order>> SaveAsync(Order order);

        Task<Response<Order>> UpdateAsync(Guid id, SaveOrderResource order);
    }
}
