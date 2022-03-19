using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Bills.Get;
using Application.Resources.Bills.Save;
using Application.Resources.OrderedCourses.Get;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BillsController : BaseController
    {
        private readonly IBillService billService;

        public BillsController(IMapper mapper, IBillService billService) : base(mapper)
        {
            this.billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await billService.ListAsync();

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Bill>, List<BillResource>>(result.Type);

            return GenerateResponse<List<BillResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillAsync(Guid id)
        {
            var result = await billService.GetBillAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Bill, BillResource>(result.Type);

            return GenerateResponse<BillResource>(result.Status, responseBody);
        }

        [HttpGet("{id}/ordered-courses")]
        public async Task<IActionResult> GetOrderedCourseForBillAsync(Guid id)
        {
            var result = await billService.GetOrderedCourseForBillAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<OrderedCourse>, List<OrderedCourseResource>>(result.Type);

            return GenerateResponse<List<OrderedCourseResource>>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveBillResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await billService.SaveAsync(resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Bill>(HttpStatusCode.NoContent, new Bill());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveBillResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await billService.UpdateAsync(id, resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Bill>(HttpStatusCode.NoContent, new Bill());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await billService.DeleteAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Bill>(HttpStatusCode.NoContent, new Bill());
        }
    }
}