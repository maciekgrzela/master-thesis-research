using Application.Resources.Products.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly IProductsCategoryRepository productsCategoryRepository;

        public ProductsService(IProductRepository productRepository, IProductsCategoryRepository productsCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.productsCategoryRepository = productsCategoryRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<Product>> GetProductAsync(Guid id)
        {
            var product = await productRepository.GetProductAsync(id);

            if (product == null)
            {
                return new Response<Product>(HttpStatusCode.NotFound, $"Product with id:{id} not found");
            }

            return new Response<Product>(HttpStatusCode.OK, product);
        }

        public async Task<Response<IEnumerable<Product>>> ListAsync()
        {
            var products = await productRepository.ListAsync();
            return new Response<IEnumerable<Product>>(HttpStatusCode.OK, products);
        }

        public async Task<Response<Product>> SaveAsync(SaveProductResource product)
        {

            var existingCategory = await productsCategoryRepository.GetProductsCategoryAsync(product.ProductsCategoryId);

            if(existingCategory == null)
            {
                return new Response<Product>(HttpStatusCode.NotFound, $"Category with id:{product.ProductsCategoryId} not found");
            }

            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Amount = product.Amount,
                Unit = product.Unit,
                ProductsCategoryId = product.ProductsCategoryId
            };

            await productRepository.SaveAsync(newProduct);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Product>(HttpStatusCode.NoContent, newProduct);
        }

        public async Task<Response<Product>> UpdateAsync(Guid id, SaveProductResource product)
        {
            var existingProduct = await productRepository.GetProductAsync(id);

            if (existingProduct == null)
            {
                return new Response<Product>(HttpStatusCode.NotFound, $"Product with id:{id} not found");
            }

            var existingCategory = await productsCategoryRepository.GetProductsCategoryAsync(product.ProductsCategoryId);

            if(existingCategory == null)
            {
                return new Response<Product>(HttpStatusCode.NotFound, $"Category with id:{product.ProductsCategoryId} not found");
            }

            existingProduct.Amount = product.Amount;
            existingProduct.Name = product.Name;
            existingProduct.Unit = product.Unit;
            existingProduct.ProductsCategoryId = product.ProductsCategoryId;

            productRepository.Update(existingProduct);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Product>(HttpStatusCode.NoContent, existingProduct);
        }
    }
}
