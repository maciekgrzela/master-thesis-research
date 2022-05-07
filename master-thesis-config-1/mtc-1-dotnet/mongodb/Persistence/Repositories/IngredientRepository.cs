using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(DataContext context) : base(context) {}

        public void Delete(Ingredient ingredient)
        {
            context.Ingredients.Remove(ingredient);
        }

        public async Task<Ingredient> GetIngredientAsync(Guid id)
        {
            return await context.Ingredients
            .Include(p => p.Product)
            .ThenInclude(p => p.ProductsCategory)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Ingredient>> ListAsync()
        {
            return await context.Ingredients
            .Include(p => p.Product)
            .ThenInclude(p => p.ProductsCategory)
            .ToListAsync();
        }

        public async Task SaveAsync(Ingredient ingredient)
        {
            await context.Ingredients.AddAsync(ingredient);
        }

        public async Task SaveRangeAsync(List<Ingredient> ingredients)
        {
            await context.Ingredients.AddRangeAsync(ingredients);
        }

        public void Update(Ingredient ingredient)
        {
            context.Ingredients.Update(ingredient);
        }
    }
}