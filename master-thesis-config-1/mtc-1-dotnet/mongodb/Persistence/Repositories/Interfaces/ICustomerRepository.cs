using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Domain.Models.MongoDb.Customer>> GetAllAsync();

        Task<Domain.Models.MongoDb.Customer> GetAsync(string id);

        Task SaveAsync(Domain.Models.MongoDb.Customer customer);

        Task Update(Domain.Models.MongoDb.Customer customer);

        Task Delete(Domain.Models.MongoDb.Customer customer);
    }
}