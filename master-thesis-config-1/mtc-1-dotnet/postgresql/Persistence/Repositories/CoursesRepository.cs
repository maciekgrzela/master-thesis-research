using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class CoursesRepository : BaseRepository, ICoursesRepository
    {
        public CoursesRepository(DataContext context) : base(context) {}

        public async Task<Course> GetCourseAsync(Guid id)
        {
            return await context.Courses
                .Include(p => p.CoursesCategory)
                .Include(p => p.Ingredients)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductsCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<System.Collections.Generic.List<Course>> ListAsync()
        {
            return await context.Courses
                .Include(p => p.CoursesCategory)
                .Include(p => p.Ingredients)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductsCategory)
                .ToListAsync();
        }

        public async Task SaveAsync(Course course)
        {
            await context.Courses.AddAsync(course);
        }

        public void Update(Course course)
        {
            context.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            context.Courses.Remove(course);
        }
    }
}