using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.CourseCategories.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ICourseCategoryService
    {
        Task<Response<IEnumerable<CoursesCategory>>> ListAsync();
        Task<Response<CoursesCategory>> GetCoursesCategoryAsync(Guid id);
        Task<Response<CoursesCategory>> SaveAsync(CoursesCategory category);
        Task<Response<CoursesCategory>> UpdateAsync(Guid id, CoursesCategory category);
        Task<Response<CoursesCategory>> DeleteAsync(Guid id);
    }
}