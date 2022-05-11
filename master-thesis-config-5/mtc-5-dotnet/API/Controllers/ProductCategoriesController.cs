using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.ProductsCategories.Get;
using Application.Resources.ProductsCategories.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/product-categories")]
    public class ProductCategoriesController : BaseController
    {
        private readonly IProductsCategoryService productsCategoryService;
        public ProductCategoriesController(IMapper mapper, IProductsCategoryService productsCategoryService) : base(mapper)
        {
            this.productsCategoryService = productsCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListAsync()
        {
            var result = await productsCategoryService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<ProductsCategory>, IEnumerable<ProductsCategoryResource>>(result.Type);

            return GenerateResponse<IEnumerable<ProductsCategoryResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsCategoryAsync(Guid id)
        {
            var result = await productsCategoryService.GetProductsCategoryAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<ProductsCategory, ProductsCategoryResource>(result.Type);

            return GenerateResponse<ProductsCategoryResource>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveProductsCategoryResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var category = mapper.Map<SaveProductsCategoryResource, ProductsCategory>(resource);
            var result = await productsCategoryService.SaveAsync(category);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<ProductsCategory>(HttpStatusCode.NoContent, new ProductsCategory());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveProductsCategoryResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var category = mapper.Map<SaveProductsCategoryResource, ProductsCategory>(resource);
            var result = await productsCategoryService.UpdateAsync(id, category);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<ProductsCategory>(HttpStatusCode.NoContent, new ProductsCategory());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await productsCategoryService.DeleteAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<ProductsCategory>(HttpStatusCode.NoContent, new ProductsCategory());
        }


    }
}