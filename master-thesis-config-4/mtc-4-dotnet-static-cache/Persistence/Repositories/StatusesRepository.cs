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
    public class StatusesRepository : BaseRepository, IStatusesRepository
    {
        public StatusesRepository(DataContext context) : base(context) { }

        public async Task<List<Status>> ListAsync()
        {
            return await context.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusAsync(Guid id)
        {
            return await context.Statuses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync(Status status)
        {
            await context.Statuses.AddAsync(status);
        }

        public void Update(Status status)
        {
            context.Statuses.Update(status);
        }

        public void Delete(Status status)
        {
            context.Statuses.Remove(status);
        }

        public async Task<Status> GetStatusByNameAsync(string status)
        {
            return await context.Statuses.FirstOrDefaultAsync(p => p.Name == status);
        }
    }
}
