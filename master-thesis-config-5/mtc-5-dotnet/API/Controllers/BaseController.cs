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

        protected IActionResult GenerateResponse<T>(HttpStatusCode status, T body)
        {
            return status switch
            {
                HttpStatusCode.OK => Ok(body),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created => Created("", body),
                HttpStatusCode.Forbidden => Forbid(body.ToString()),
                HttpStatusCode.Unauthorized => Unauthorized(body),
                HttpStatusCode.NotFound => NotFound(body),
                _ => BadRequest(body)
            };
        }

    }
}