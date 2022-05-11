using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer> GetAsync(Guid id);

        Task SaveAsync(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);
    }
}