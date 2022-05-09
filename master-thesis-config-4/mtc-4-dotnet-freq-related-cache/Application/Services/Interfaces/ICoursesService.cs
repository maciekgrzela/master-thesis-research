using Application.Resources.Course.Save;
using Application.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICoursesService
    {
        Task<Response<List<Course>>> ListAsync();
        Task<Response<Course>> GetCourseAsync(Guid id);
        Task<Response<List<Ingredient>>> GetIngredientsForCourseAsync(Guid id);
        Task<Response<Course>> SaveAsync(SaveCourseResource status);
        Task<Response<Course>> UpdateAsync(Guid id, SaveCourseResource status);
        Task<Response<Course>> DeleteAsync(Guid id);
    }
}
