using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;

        public BaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult GenerateResponse<T>(HttpStatusCode status, T body) {
            switch(status) {
                case HttpStatusCode.OK:
                    return Ok(body);
                case HttpStatusCode.NoContent:
                    return NoContent();
                case HttpStatusCode.Created:
                    return Created("", body);
                case HttpStatusCode.Forbidden:
                    return Forbid(body.ToString());
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(body);
                case HttpStatusCode.NotFound:
                    return NotFound(body);
                default:
                    return BadRequest(body);
            }
        }

    }
}