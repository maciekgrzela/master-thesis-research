using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class StatusEntriesRepository : BaseRepository, IStatusEntriesRepository
    {
        public StatusEntriesRepository(DataReadContext context) : base(context) { }

        public async Task<IEnumerable<StatusEntry>> ListAsync()
        {
            return await context.StatusEntries.ToListAsync();
        }

        public async Task<StatusEntry> GetStatusEntryAsync(Guid id)
        {
            return await context.StatusEntries.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(StatusEntry status)
        {
            await context.StatusEntries.AddAsync(status);
        }

        public void Update(StatusEntry status)
        {
            context.StatusEntries.Update(status);
        }

        public void Delete(StatusEntry status)
        {
            context.StatusEntries.Remove(status);
        }
    }
}
