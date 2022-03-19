using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IStatusesRepository
    {
        Task<List<Status>> ListAsync();
        Task<Status> GetStatusAsync(Guid id);
        Task<Status> GetStatusByNameAsync(string status);
        Task SaveAsync(Status status);
        void Update(Status status);
        void Delete(Status status);
    }
}
