using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.OrderedCourses.Get;
using Application.Resources.OrderedCourses.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/ordered-courses")]
    public class OrderedCoursesController : BaseController
    {
        private readonly IOrderedCourseService orderedCourseService;
        public OrderedCoursesController(IMapper mapper, IOrderedCourseService orderedCourseService) : base(mapper)
        {
            this.orderedCourseService = orderedCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync() 
        {
            var result = await orderedCourseService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<List<OrderedCourse>, List<OrderedCourseResource>>(result.Type);

            return GenerateResponse<List<OrderedCourseResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderedCourseAsync(Guid id)
        {
            var result = await orderedCourseService.GetOrderedCourseAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<OrderedCourse, OrderedCourseResource>(result.Type);

            return GenerateResponse<OrderedCourseResource>(result.Status, responseBody);
        }
        

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] List<SaveOrderedCourseResource> resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var orderedCourse = mapper.Map<List<SaveOrderedCourseResource>, List<OrderedCourse>>(resource);
            var result = await orderedCourseService.SaveAsync(orderedCourse);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<OrderedCourse>(HttpStatusCode.NoContent, new OrderedCourse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveOrderedCourseResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var orderedCourse = mapper.Map<SaveOrderedCourseResource, OrderedCourse>(resource);
            var result = await orderedCourseService.UpdateAsync(id, orderedCourse);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<OrderedCourse>(HttpStatusCode.NoContent, new OrderedCourse());
        }

        [HttpPut("{orderedCourseId}/status")]
        public async Task<IActionResult> UpdateStatusAsync([FromRoute] Guid orderedCourseId, [FromBody] string statusName)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await orderedCourseService.UpdateStatusAsync(orderedCourseId, statusName);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<OrderedCourse>(HttpStatusCode.NoContent, new OrderedCourse());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await orderedCourseService.DeleteAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<OrderedCourse>(HttpStatusCode.NoContent, new OrderedCourse());

        }
    }
}