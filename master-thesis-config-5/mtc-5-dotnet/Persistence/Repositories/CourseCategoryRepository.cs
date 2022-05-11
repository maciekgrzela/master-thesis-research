using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class CourseCategoryRepository : BaseRepository, ICourseCategoryRepository
    {
        public CourseCategoryRepository(DataReadContext context) : base(context) {}

        public void Delete(CoursesCategory category)
        {
            context.CourseCategories.Remove(category);
        }

        public async Task<CoursesCategory> GetCoursesCategoryAsync(Guid id)
        {
            return await context.CourseCategories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<CoursesCategory>> ListAsync()
        {
            return await context.CourseCategories.ToListAsync();
        }

        public async Task SaveAsync(CoursesCategory category)
        {
            await context.CourseCategories.AddAsync(category);
        }

        public void Update(CoursesCategory category)
        {
            context.CourseCategories.Update(category);
        }
    }
}