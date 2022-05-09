using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid id);
        Task<List<Product>> ListAsync();
        Task SaveAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}