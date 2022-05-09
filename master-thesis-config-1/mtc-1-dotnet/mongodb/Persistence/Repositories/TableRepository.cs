using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly DataContext context;

        public TableRepository(DataContext context)
        {
            this.context = context;
        }

        public void Delete(Table table)
        {
            context.Tables.Remove(table);
        }

        public async Task<List<Table>> GetAllAsync()
        {
            return await context.Tables
                            .Include(p => p.Orders)
                            .ToListAsync();
        }

        public async Task<List<Table>> GetAllForHallAsync(Guid hallId)
        {
            return await context.Tables
                .Where(t => t.HallId == hallId)
                .ToListAsync();
        }

        public async Task<Table> GetAsync(Guid id)
        {
            return await context.Tables.Include(p => p.Orders).ThenInclude(o => o.StatusEntries).ThenInclude(e => e.Status).FirstOrDefaultAsync(t => t.Id == id);        }

        public async Task SaveAsync(Table table)
        {
            await context.Tables.AddAsync(table);
        }

        public void Update(Table table)
        {
            context.Tables.Update(table);
        }
    }
}