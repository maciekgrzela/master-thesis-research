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
    public class ProductsCategoryService : IProductsCategoryService
    {
        private readonly IProductsCategoryRepository productsCategoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductsCategoryService(IUnitOfWork unitOfWork, IProductsCategoryRepository productsCategoryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.productsCategoryRepository = productsCategoryRepository;
        }

        public async Task<Response<ProductsCategory>> DeleteAsync(Guid id)
        {
            var existingProductsCategory = await productsCategoryRepository.GetProductsCategoryAsync(id);

            if (existingProductsCategory == null)
            {
                return new Response<ProductsCategory>(HttpStatusCode.NotFound, $"Products Category with id:{id} not found");
            }

            productsCategoryRepository.Delete(existingProductsCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<ProductsCategory>(HttpStatusCode.NoContent, existingProductsCategory);
        }

        public async Task<Response<ProductsCategory>> GetProductsCategoryAsync(Guid id)
        {
            var existingProductsCategory = await productsCategoryRepository.GetProductsCategoryAsync(id);

            if (existingProductsCategory == null)
            {
                return new Response<ProductsCategory>(HttpStatusCode.NotFound, $"Bill with id:{id} not found");
            }

            return new Response<ProductsCategory>(HttpStatusCode.OK, existingProductsCategory);
        }

        public async Task<Response<List<ProductsCategory>>> ListAsync()
        {
            var productsCategories = await productsCategoryRepository.ListAsync();
            return new Response<List<ProductsCategory>>(HttpStatusCode.OK, productsCategories);
        }

        public async Task<Response<ProductsCategory>> SaveAsync(ProductsCategory category)
        {
            var productsCategory = new ProductsCategory
            {
                Id = Guid.NewGuid(),
                Name = category.Name
            };

            await productsCategoryRepository.SaveAsync(productsCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<ProductsCategory>(HttpStatusCode.NoContent, productsCategory);
        }

        public async Task<Response<ProductsCategory>> UpdateAsync(Guid id, ProductsCategory category)
        {
            var existingProductsCategory = await productsCategoryRepository.GetProductsCategoryAsync(id);

            if(existingProductsCategory == null)
            {
                return new Response<ProductsCategory>(HttpStatusCode.NotFound, $"Products Category with id:{id} not found");
            }

            existingProductsCategory.Name = category.Name;

            productsCategoryRepository.Update(existingProductsCategory);
            await unitOfWork.CommitTransactionAsync();

            return new Response<ProductsCategory>(HttpStatusCode.NoContent, existingProductsCategory);
        }
    }
}