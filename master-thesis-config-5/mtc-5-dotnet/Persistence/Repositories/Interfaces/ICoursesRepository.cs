using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        Task<Course> GetCourseAsync(Guid id);
        Task<IEnumerable<Course>> ListAsync();
        Task SaveAsync(Course course);
        void Update(Course course);
        void Delete(Course course);
    }
}