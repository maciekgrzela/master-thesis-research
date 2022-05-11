using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<Response<IEnumerable<Ingredient>>> ListAsync();
        Task<Response<Ingredient>> GetIngredientAsync(Guid id);
        Task<Response<Ingredient>> SaveAsync(Ingredient ingredient);
        Task<Response<Ingredient>> UpdateAsync(Guid id, Ingredient ingredient);
        Task<Response<Ingredient>> DeleteAsync(Guid id);
    }
}