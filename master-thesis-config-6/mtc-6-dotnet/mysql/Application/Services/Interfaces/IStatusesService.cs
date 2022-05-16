using Application.Resources.Statuses.Save;
using Application.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IStatusesService
    {
        Task<Response<List<Status>>> ListAsync();
        Task<Response<Status>> GetStatusAsync(Guid id);
        Task<Response<Status>> SaveAsync(SaveStatusResource status);
        Task<Response<Status>> UpdateAsync(Guid id, SaveStatusResource status);
        Task<Response<Status>> DeleteAsync(Guid id);
    }
}
