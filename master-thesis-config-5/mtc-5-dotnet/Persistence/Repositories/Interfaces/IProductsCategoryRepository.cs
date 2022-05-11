using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IProductsCategoryRepository
    {
        Task<IEnumerable<ProductsCategory>> ListAsync();
        Task<ProductsCategory> GetProductsCategoryAsync(Guid id);
        Task SaveAsync(ProductsCategory category);
        void Update(ProductsCategory category);
        void Delete(ProductsCategory category);
    }
}