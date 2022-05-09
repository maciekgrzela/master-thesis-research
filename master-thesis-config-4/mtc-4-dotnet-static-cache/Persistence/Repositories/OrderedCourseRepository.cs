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
    public class OrderedCourseRepository : BaseRepository, IOrderedCourseRepository
    {
        public OrderedCourseRepository(DataContext context) : base(context) {}

        public async Task<List<OrderedCourse>> ListAsync()
        {
            return await context.OrderedCourses
            .Include(p => p.Course)
            .ThenInclude(p => p.CoursesCategory)
            .Include(p => p.StatusEntries)
            .ThenInclude(p => p.Status)
            .Include(p => p.Order)
            .Include(p => p.Bill)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<List<OrderedCourse>> GetCoursesForBillId(Guid id)
        {
            return await context.OrderedCourses
            .Include(p => p.Course)
            .ThenInclude(p => p.CoursesCategory)
            .Include(p => p.StatusEntries)
            .ThenInclude(p => p.Status)
            .Include(p => p.Order)
            .Include(p => p.Bill)
            .AsSplitQuery()
            .Where(p => p.BillId == id)
            .ToListAsync();
        }

        public async Task<OrderedCourse> GetOrderedCourseAsync(Guid id)
        {
            return await context.OrderedCourses
            .Include(p => p.Course)
            .ThenInclude(p => p.CoursesCategory)
            .Include(p => p.StatusEntries)
            .ThenInclude(p => p.Status)
            .Include(p => p.Order)
            .Include(p => p.Bill)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(OrderedCourse orderedCourse)
        {
            await context.OrderedCourses.AddAsync(orderedCourse);
        }

        public void Update(OrderedCourse orderedCourse)
        {
            context.OrderedCourses.Update(orderedCourse);
        }

        public void Delete(OrderedCourse orderedCourse)
        {
            context.OrderedCourses.Remove(orderedCourse);
        }
    }
}