using System;
using System.Threading.Tasks;
using Application.CQRS.Courses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/cqrs/courses")]
    public class CqrsCoursesController : BaseController
    {
        private readonly IMediator mediator;

        public CqrsCoursesController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            this.mediator = mediator;
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCourse.Command data)
        {
            data.SetId(id);
            var courseUpdated = await mediator.Send(data);
            return GenerateResponse(courseUpdated.Status, courseUpdated.Message);
        }
    }
}