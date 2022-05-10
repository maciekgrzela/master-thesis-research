using System.Threading.Tasks;
using Application.CQRS.Bills;
using Application.Params;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cqrs/bills")]
    public class CqrsBillsController : BaseController
    {
        private readonly IMediator mediator;

        public CqrsBillsController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListAsync([FromQuery] PagingParams queryParams)
        {
            var bills = await mediator.Send(new GetBills.Query { QueryParams = queryParams });
            return GenerateResponse(bills.Status, bills.Type);
        }
    }
}