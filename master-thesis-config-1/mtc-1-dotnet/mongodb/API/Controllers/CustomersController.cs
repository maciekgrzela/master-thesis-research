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
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await customerService.GetAllAsync();

            if(!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Domain.Models.MongoDb.Customer>, List<CustomerResource>>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await customerService.GetAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Domain.Models.MongoDb.Customer, CustomerResource>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCustomerResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await customerService.SaveAsync(resource);

            return !result.Success ? GenerateResponse(result.Status, result.Message) : GenerateResponse(HttpStatusCode.NoContent, new Customer());
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] SaveCustomerResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await customerService.Update(id, resource);

            return !result.Success ? GenerateResponse(result.Status, result.Message) : GenerateResponse(HttpStatusCode.NoContent, new Customer());
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await customerService.Delete(id);

            return !result.Success ? GenerateResponse(result.Status, result.Message) : GenerateResponse(HttpStatusCode.NoContent, new Customer());
        }
    }
}