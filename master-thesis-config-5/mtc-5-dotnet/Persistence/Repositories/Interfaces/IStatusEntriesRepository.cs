using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IStatusEntriesRepository
    {
        Task<IEnumerable<StatusEntry>> ListAsync();
        Task<StatusEntry> GetStatusEntryAsync(Guid id);
        Task SaveAsync(StatusEntry status);
        void Update(StatusEntry status);
        void Delete(StatusEntry status);
    }
}
