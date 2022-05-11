using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Params;
using Application.Resources.Bills.Get;
using Application.Resources.Bills.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IBillService
    {
        Task<Response<PagedList<Bill>>> ListAsync(PagingParams queryParams);
        Task<Response<Bill>> GetBillAsync(Guid id);
        Task<Response<IEnumerable<OrderedCourse>>> GetOrderedCourseForBillAsync(Guid id);
        Task<Response<Bill>> SaveAsync(SaveBillResource bill);
        Task<Response<Bill>> UpdateAsync(Guid id, SaveBillResource bill);
        Task<Response<Bill>> DeleteAsync(Guid id);
    }
}