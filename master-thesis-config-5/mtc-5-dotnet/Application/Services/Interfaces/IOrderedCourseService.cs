using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Enums;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IOrderedCourseService
    {
         Task<Response<IEnumerable<OrderedCourse>>> ListAsync();
         Task<Response<OrderedCourse>> GetOrderedCourseAsync(Guid id);
         Task<Response<OrderedCourse>> SaveAsync(IEnumerable<OrderedCourse> orderedCourses);
         Task<Response<OrderedCourse>> ModifyStatusAsync(OrderedCourse orderedCourse, StatusNameEnum status, string note);
         Task<Response<OrderedCourse>> UpdateAsync(Guid id, OrderedCourse orderedCourse);
         Task<Response<OrderedCourse>> UpdateStatusAsync(Guid orderedCourseId, string statusName);
         Task<Response<OrderedCourse>> DeleteAsync(Guid id);
    }
}