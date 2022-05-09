using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Reservation.Get;
using Application.Resources.Table.Get;
using Application.Resources.Table.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/[controller]")]

    public class TablesController : BaseController
    {
        private readonly ITableService tableService;
        private readonly IReservationService reservationService;
        public TablesController(IMapper mapper, ITableService tableService, IReservationService reservationService) : base(mapper)
        {
            this.tableService = tableService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await tableService.GetAllAsync();

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Table>, List<TableResource>>(result.Type);

            return GenerateResponse<List<TableResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await tableService.GetAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Table, TableResource>(result.Type);

            return GenerateResponse<TableResource>(result.Status, responseBody);
        }

        [HttpGet("{id}/reservations")]
        public async Task<IActionResult> GetReservationsAsync(Guid id)
        {
            var result = await reservationService.SearchByTableAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<Reservation>, List<ReservationResource>>(result.Type);

            return GenerateResponse<List<ReservationResource>>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTableResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await tableService.SaveAsync(resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Table>(HttpStatusCode.NoContent, new Table());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveTableResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await tableService.Update(id, resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Table>(HttpStatusCode.NoContent, new Table());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await tableService.Delete(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Table>(HttpStatusCode.NoContent, new Table());
        }
    }
}