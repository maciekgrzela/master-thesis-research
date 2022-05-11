using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ICourseCategoryRepository
    {
        Task<IEnumerable<CoursesCategory>> ListAsync();
        Task<CoursesCategory> GetCoursesCategoryAsync(Guid id);
        Task SaveAsync(CoursesCategory category);
        void Update(CoursesCategory category);
        void Delete(CoursesCategory category);
    }
}