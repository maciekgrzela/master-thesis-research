using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IOrderedCourseRepository
    {
         Task<IEnumerable<OrderedCourse>> ListAsync();
         Task<IEnumerable<OrderedCourse>> GetCoursesForBillId(Guid id);
         Task<OrderedCourse> GetOrderedCourseAsync(Guid id);
         Task SaveAsync(OrderedCourse orderedCourse);
         void Update(OrderedCourse orderedCourse);
         void Delete(OrderedCourse orderedCourse);
    }
}