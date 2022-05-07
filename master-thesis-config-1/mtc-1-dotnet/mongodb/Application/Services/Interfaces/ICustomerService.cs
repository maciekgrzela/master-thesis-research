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
        Task<Response<List<Domain.Models.MongoDb.Customer>>> GetAllAsync();

        Task<Response<Domain.Models.MongoDb.Customer>> GetAsync(string id);

        Task<Response<Domain.Models.MongoDb.Customer>> SaveAsync(SaveCustomerResource customer);

        Task<Response<Domain.Models.MongoDb.Customer>> Update(string id, SaveCustomerResource customer);

        Task<Response<Domain.Models.MongoDb.Customer>> Delete(string id);
    }
}