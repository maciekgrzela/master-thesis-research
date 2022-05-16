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

        protected IActionResult GenerateResponse<T>(HttpStatusCode status, T body) {
            return status switch
            {
                HttpStatusCode.OK => Ok(body),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created => Created("", body),
                HttpStatusCode.Forbidden => Forbid(body.ToString()),
                HttpStatusCode.Unauthorized => Unauthorized(body),
                HttpStatusCode.NotFound => NotFound(body),
                _ => BadRequest(body),
            };
        }

        protected IActionResult GenerateExecutionTimeResponse(HttpStatusCode status, long executionTime)
        {
            return status switch
            {
                HttpStatusCode.OK => Ok(executionTime),
                HttpStatusCode.NoContent => Ok(executionTime),
                HttpStatusCode.Created => Created("", executionTime),
                HttpStatusCode.Forbidden => Forbid(executionTime.ToString()),
                HttpStatusCode.Unauthorized => Unauthorized(executionTime),
                HttpStatusCode.NotFound => NotFound(executionTime),
                _ => BadRequest(executionTime)
            };
        }
    }
}