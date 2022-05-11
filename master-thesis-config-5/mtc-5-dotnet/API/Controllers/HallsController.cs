using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Hall.Get;
using Application.Resources.Hall.Save;
using Application.Resources.Table.Get;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]

    public class HallsController : BaseController
    {
        private readonly IHallService hallService;
        private readonly ITableService tableService;

        public HallsController(IMapper mapper, IHallService hallService, ITableService tableService) : base(mapper)
        {
            this.hallService = hallService;
            this.tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await hallService.GetAllAsync();

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Hall>, IEnumerable<HallResource>>(result.Type);

            return GenerateResponse<IEnumerable<HallResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await hallService.GetAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Hall, HallResource>(result.Type);

            return GenerateResponse<HallResource>(result.Status, responseBody);
        }

        [HttpGet("{id}/tables")]
        public async Task<IActionResult> GetTablesAsync(Guid id)
        {
            var result = await tableService.GetAllForHallAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Table>, IEnumerable<TableForHallResource>>(result.Type);

            return GenerateResponse<IEnumerable<TableForHallResource>>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveHallResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await hallService.SaveAsync(resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Hall>(HttpStatusCode.NoContent, new Hall());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveHallResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await hallService.Update(id, resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Hall>(HttpStatusCode.NoContent, new Hall());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await hallService.Delete(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Hall>(HttpStatusCode.NoContent, new Hall());
        }
    }
}