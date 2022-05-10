using System;
using System.Threading.Tasks;
using Application.CQRS.Reservations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cqrs/reservations")]
    public class CqrsReservationsController : BaseController
    {
        private readonly IMediator mediator;

        public CqrsReservationsController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            this.mediator = mediator;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var reservationDeleted = await mediator.Send(new DeleteReservation.Command { Id = id });
            return GenerateResponse(reservationDeleted.Status, reservationDeleted.Type);
        }
    }
}