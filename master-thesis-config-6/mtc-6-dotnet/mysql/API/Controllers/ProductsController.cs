using Application.Extensions;
using Application.Resources.Products.Get;
using Application.Resources.Products.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;

        public ProductsController(IMapper mapper, IProductsService productsService) : base(mapper)
        {
            this.productsService = productsService;
        }


        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await productsService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Product>, List<ProductResource>>(result.Type);

            return GenerateResponse<List<ProductResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            var result = await productsService.GetProductAsync(id);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Product, ProductResource>(result.Type);

            return GenerateResponse<ProductResource>(result.Status, responseBody);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveAsync([FromBody] SaveProductResource resource)
        {
            var timer = new Stopwatch();
            timer.Start();
            if (!ModelState.IsValid)
            {
                timer.Stop();
                return GenerateExecutionTimeResponse(HttpStatusCode.BadRequest, timer.ElapsedMilliseconds);
            }

            var result = await productsService.SaveAsync(resource);

            if (!result.Success)
            {
                timer.Stop();
                return GenerateExecutionTimeResponse(result.Status, timer.ElapsedMilliseconds);
            }

            var productResource = mapper.Map<Product, SaveProductResource>(result.Type);
            timer.Stop();
            return GenerateExecutionTimeResponse(HttpStatusCode.Created, timer.ElapsedMilliseconds);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await productsService.UpdateAsync(id, resource);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Product>(HttpStatusCode.NoContent, new Product());
        }

    }
}
