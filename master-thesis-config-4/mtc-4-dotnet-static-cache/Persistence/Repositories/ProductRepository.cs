using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) {}

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await context.Products.Include(p => p.ProductsCategory).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> ListAsync()
        {
            return await context.Products.Include(p => p.ProductsCategory).ToListAsync();
        }

        public async Task SaveAsync(Product product)
        {
            await context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
        }
    }
}