using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.CourseCategories.Get;
using Application.Resources.CourseCategories.Save;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/course-categories")]
    public class CourseCategoriesController : BaseController
    {
        private readonly ICourseCategoryService courseCategoryService;
        public CourseCategoriesController(IMapper mapper, ICourseCategoryService courseCategoryService) : base(mapper)
        {
            this.courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await courseCategoryService.ListAsync();

            if (!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<IEnumerable<CoursesCategory>, IEnumerable<CoursesCategoryResource>>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoursesCategoryAsync(Guid id)
        {
            var result = await courseCategoryService.GetCoursesCategoryAsync(id);
            
            if(!result.Success)
            {
                return GenerateResponse<string>(result.Status, result.Message);
            }

            var responseBody = mapper.Map<CoursesCategory, CoursesCategoryResource>(result.Type);

            return GenerateResponse(result.Status, responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveCoursesCategoryResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var category = mapper.Map<SaveCoursesCategoryResource, CoursesCategory>(resource);
            var result = await courseCategoryService.SaveAsync(category);

            if(!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            return GenerateResponse(HttpStatusCode.NoContent, new CoursesCategory());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SaveCoursesCategoryResource resource)
        {
            if(!ModelState.IsValid)
            {
                return GenerateResponse(HttpStatusCode.BadRequest, ModelState.GetErrorMessages());
            }

            var category = mapper.Map<SaveCoursesCategoryResource, CoursesCategory>(resource);
            var result = await courseCategoryService.UpdateAsync(id, category);

            if(!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            return GenerateResponse(HttpStatusCode.NoContent, new CoursesCategory());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await courseCategoryService.DeleteAsync(id);

            if(!result.Success)
            {
                return GenerateResponse(result.Status, result.Message);
            }

            return GenerateResponse(HttpStatusCode.NoContent, new CoursesCategory());
        }
    }
}