using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> ListAsync();
        Task<Ingredient> GetIngredientAsync(Guid id);
        Task SaveAsync(Ingredient ingredient);
        Task SaveRangeAsync(List<Ingredient> ingredients);
        void Update(Ingredient ingredient);
        void Delete(Ingredient ingredient);
    }
}