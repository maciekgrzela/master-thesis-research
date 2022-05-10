using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Task<List<Bill>> ListAsync();
        IQueryable<Bill> QueryableAsync();
        Task<Bill> GetBillAsync(Guid id);
        Task SaveAsync(Bill bill);
        void Update(Bill bill);
        void Delete(Bill bill);
    }
}