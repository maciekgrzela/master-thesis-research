using System.Threading.Tasks;
using Application.CQRS.Products;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cqrs/products")]
    public class CqrsProductsController : BaseController
    {
        private readonly IMediator mediator;

        public CqrsProductsController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddAsync([FromBody] CreateProduct.Command data)
        {
            var productCreated = await mediator.Send(data);
            return GenerateResponse(productCreated.Status, productCreated.Message);
        }
    }
}