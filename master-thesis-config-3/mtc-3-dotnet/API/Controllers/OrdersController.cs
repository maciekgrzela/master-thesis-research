using Application.Extensions;
using Application.Resources.Orders.Get;
using Application.Resources.Orders.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IMapper mapper, IOrdersService ordersService) : base(mapper)
        {
            this.ordersService = ordersService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListAsync()
        {
            var result = await ordersService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Order>, List<OrderResource>>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderAsync(Guid id)
        {
            var result = await ordersService.GetOrderAsync(id);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Order, OrderResource>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("tables/{id}/last-order")]
        public async Task<IActionResult> GetTableLastOrderAsync(Guid id)
        {
            var result = await ordersService.GetLastTableOrderAsync(id);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Order, OrderResource>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("tables/{id}/orders")]
        public async Task<IActionResult> GetTableOrdersAsync(Guid id)
        {
            var result = await ordersService.GetTableOrdersAsync(id);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Order>, List<OrderResource>>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserOrdersAsync(string id)
        {
            var result = await ordersService.GetUserOrdersAsync(id);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Order>, List<OrderResource>>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var order = mapper.Map<SaveOrderResource, Order>(resource);
            var result = await ordersService.SaveAsync(order);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            return GenerateResponse(HttpStatusCode.NoContent, new Order());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await ordersService.UpdateAsync(id, resource);

            if (!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            return GenerateResponse(HttpStatusCode.NoContent, new Order());
        }
        
    }
}
