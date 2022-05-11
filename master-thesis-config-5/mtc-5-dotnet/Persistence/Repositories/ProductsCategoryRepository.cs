using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class ProductsCategoryRepository : BaseRepository, IProductsCategoryRepository
    {
        public ProductsCategoryRepository(DataReadContext context) : base(context) {}

        public void Delete(ProductsCategory category)
        {
            context.ProductsCategories.Remove(category);
        }

        public async Task<ProductsCategory> GetProductsCategoryAsync(Guid id)
        {
            return await context.ProductsCategories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductsCategory>> ListAsync()
        {
            return await context.ProductsCategories.ToListAsync();
        }

        public async Task SaveAsync(ProductsCategory category)
        {
            await context.ProductsCategories.AddAsync(category);
        }

        public void Update(ProductsCategory category)
        {
            context.ProductsCategories.Update(category);
        }
    }
}