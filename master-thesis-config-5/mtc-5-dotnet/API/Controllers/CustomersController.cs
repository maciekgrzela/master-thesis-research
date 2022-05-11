using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Customers.Get;
using Application.Resources.Customers.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly ICustomerService customerService;

        public CustomersController(IMapper mapper, ICustomerService customerService) : base(mapper)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await customerService.GetAllAsync();

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(result.Type);

            return GenerateResponse<IEnumerable<CustomerResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await customerService.GetAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Customer, CustomerResource>(result.Type);

            return GenerateResponse<CustomerResource>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCustomerResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await customerService.SaveAsync(resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Customer>(HttpStatusCode.NoContent, new Customer());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveCustomerResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await customerService.Update(id, resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Customer>(HttpStatusCode.NoContent, new Customer());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await customerService.Delete(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Customer>(HttpStatusCode.NoContent, new Customer());
        }
    }
}