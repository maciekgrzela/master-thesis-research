using System;
using System.Threading.Tasks;
using Application.CQRS.Orders;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cqrs/orders")]
    public class CqrsOrdersController : BaseController
    {
        private readonly IMediator mediator;

        public CqrsOrdersController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOneAsync(Guid id)
        {
            var order = await mediator.Send(new GetSingleOrder.Query { Id = id });
            return GenerateResponse(order.Status, order.Type);
        }
    }
}