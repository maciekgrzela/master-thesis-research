using Application.Extensions;
using Application.Resources.Statuses.Get;
using Application.Resources.Statuses.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
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
    public class StatusesController : BaseController
    {
        private readonly IStatusesService statusService;

        public StatusesController(IMapper mapper, IStatusesService statusService) : base(mapper)
        {
            this.statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await statusService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Status>, List<StatusResource>>(result.Type);

            return GenerateResponse<List<StatusResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusAsync(Guid id)
        {
            var result = await statusService.GetStatusAsync(id);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Status, StatusResource>(result.Type);

            return GenerateResponse<StatusResource>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveStatusResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await statusService.SaveAsync(resource);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Status>(HttpStatusCode.NoContent, new Status());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveStatusResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await statusService.UpdateAsync(id, resource);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Status>(HttpStatusCode.NoContent, new Status());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await statusService.DeleteAsync(id);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Status>(HttpStatusCode.NoContent, new Status());
        }
    }
}
