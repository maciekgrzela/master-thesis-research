using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IProductsCategoryService
    {
        Task<Response<List<ProductsCategory>>> ListAsync();
        Task<Response<ProductsCategory>> GetProductsCategoryAsync(Guid id);
        Task<Response<ProductsCategory>> SaveAsync(ProductsCategory category);
        Task<Response<ProductsCategory>> UpdateAsync(Guid id, ProductsCategory category);
        Task<Response<ProductsCategory>> DeleteAsync(Guid id);
    }
}