using Application.Extensions;
using Application.Resources.Course.Get;
using Application.Resources.Course.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(IMapper mapper, ICoursesService coursesService) : base(mapper)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await coursesService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(result.Type);

            return GenerateResponse<IEnumerable<CourseResource>>(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseAsync(Guid id)
        {
            var result = await coursesService.GetCourseAsync(id);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<Course, CourseResource>(result.Type);

            return GenerateResponse<CourseResource>(result.Status, responseBody);
        }

        [HttpGet("{id}/ingredients")]
        public async Task<IActionResult> GetIngredientsForCourseAsync(Guid id)
        {
            var result = await coursesService.GetIngredientsForCourseAsync(id);

            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientForCourseResource>>(result.Type);

            return GenerateResponse<IEnumerable<IngredientForCourseResource>>(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await coursesService.SaveAsync(resource);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Course>(HttpStatusCode.NoContent, new Course());
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var result = await coursesService.UpdateAsync(id, resource);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Course>(HttpStatusCode.NoContent, new Course());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await coursesService.DeleteAsync(id);

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            return GenerateResponse<Course>(HttpStatusCode.NoContent, new Course());
        }
    }
}
