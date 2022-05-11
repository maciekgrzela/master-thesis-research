using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Reservation.Get;
using Application.Resources.Reservation.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
       [Route("api/[controller]")]

    public class ReservationsController : BaseController
    {
        private readonly IReservationService reservationService;
        public ReservationsController(IMapper mapper, IReservationService reservationService) : base(mapper)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await reservationService.GetAllAsync();

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResource>>(result.Type);

            return GenerateResponse<IEnumerable<ReservationResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await reservationService.GetAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Reservation, ReservationResource>(result.Type);

            return GenerateResponse<ReservationResource>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveReservationResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await reservationService.SaveAsync(resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Reservation>(HttpStatusCode.NoContent, new Reservation());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveReservationResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }
            
            var result = await reservationService.Update(id, resource);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Reservation>(HttpStatusCode.NoContent, new Reservation());
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await reservationService.Delete(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Reservation>(HttpStatusCode.NoContent, new Reservation());
        }
    }
}