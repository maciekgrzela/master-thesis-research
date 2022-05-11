using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Customers.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Response<IEnumerable<Customer>>> GetAllAsync();

        Task<Response<Customer>> GetAsync(Guid id);

        Task<Response<Customer>> SaveAsync(SaveCustomerResource customer);

        Task<Response<Customer>> Update(Guid id, SaveCustomerResource customer);

        Task<Response<Customer>> Delete(Guid id);
    }
}