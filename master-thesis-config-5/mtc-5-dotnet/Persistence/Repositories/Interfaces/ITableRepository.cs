using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();

        Task<IEnumerable<Table>> GetAllForHallAsync(Guid hallId);

        Task<Table> GetAsync(Guid id);

        Task SaveAsync(Table table);

        void Update(Table table);

        void Delete(Table table);
    }
}