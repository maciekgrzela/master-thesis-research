using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IProductRepository productRepository;

        public IngredientService(IIngredientRepository ingredientRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.ingredientRepository = ingredientRepository;
            this.unitOfWork = unitOfWork;
        }


        public async Task<Response<Ingredient>> DeleteAsync(Guid id)
        {
            var existingIngredient = await ingredientRepository.GetIngredientAsync(id);

            if (existingIngredient == null)
            {
                return new Response<Ingredient>(HttpStatusCode.NotFound, $"Ingredient with id:{id} not found");
            }

            ingredientRepository.Delete(existingIngredient);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Ingredient>(HttpStatusCode.NoContent, existingIngredient);
        }

        public async Task<Response<Ingredient>> GetIngredientAsync(Guid id)
        {
            var existingIngredient = await ingredientRepository.GetIngredientAsync(id);

            if (existingIngredient == null)
            {
                return new Response<Ingredient>(HttpStatusCode.NotFound, $"Ingredient with id:{id} not found");
            }

            return new Response<Ingredient>(HttpStatusCode.OK, existingIngredient);
        }

        public async Task<Response<IEnumerable<Ingredient>>> ListAsync()
        {
            var ingredients = await ingredientRepository.ListAsync();
            return new Response<IEnumerable<Ingredient>>(HttpStatusCode.OK, ingredients);
        }

        public async Task<Response<Ingredient>> SaveAsync(Ingredient ingredient)
        {

            var existingProduct = await productRepository.GetProductAsync(ingredient.ProductId);

            if (existingProduct == null)
            {
                return new Response<Ingredient>(HttpStatusCode.NotFound, $"Product with id:{ingredient.ProductId} not found");
            }

            var savedIngredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Amount = ingredient.Amount,
                Product = existingProduct
            };

            await ingredientRepository.SaveAsync(savedIngredient);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Ingredient>(HttpStatusCode.NoContent, savedIngredient);
        }

        public async Task<Response<Ingredient>> UpdateAsync(Guid id, Ingredient ingredient)
        {
            var existingIngredient = await ingredientRepository.GetIngredientAsync(id);

            if (existingIngredient == null)
            {
                return new Response<Ingredient>(HttpStatusCode.NotFound, $"Ingredient with id:{id} not found");
            }

            var existingProduct = await productRepository.GetProductAsync(ingredient.ProductId);

            if (existingProduct == null)
            {
                return new Response<Ingredient>(HttpStatusCode.NotFound, $"Product with id:{ingredient.ProductId} not found");
            }

            existingIngredient.Amount = ingredient.Amount;
            existingIngredient.Product = existingProduct;

            ingredientRepository.Update(existingIngredient);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Ingredient>(HttpStatusCode.NoContent, existingIngredient);
        }
    }
}