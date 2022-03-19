using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Bills.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IBillService
    {
        Task<Response<List<Bill>>> ListAsync();
        Task<Response<Bill>> GetBillAsync(Guid id);
        Task<Response<List<OrderedCourse>>> GetOrderedCourseForBillAsync(Guid id);
        Task<Response<Bill>> SaveAsync(SaveBillResource bill);
        Task<Response<Bill>> UpdateAsync(Guid id, SaveBillResource bill);
        Task<Response<Bill>> DeleteAsync(Guid id);
    }
}