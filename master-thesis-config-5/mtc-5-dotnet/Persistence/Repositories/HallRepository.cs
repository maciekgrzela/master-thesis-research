using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly DataReadContext context;

        public HallRepository(DataReadContext context)
        {
            this.context = context;
        }

        public void Delete(Hall hall)
        {
            context.Halls.Remove(hall);
        }

        public async Task<IEnumerable<Hall>> GetAllAsync()
        {
            return await context.Halls.ToListAsync();
        }

        public async Task<Hall> GetAsync(Guid id)
        {
            return await context.Halls.FirstOrDefaultAsync(h => h.Id == id);                
        }

        public async Task SaveAsync(Hall hall)
        {
            await context.Halls.AddAsync(hall);
        }

        public void Update(Hall hall)
        {
            context.Halls.Update(hall);
        }
    }
}