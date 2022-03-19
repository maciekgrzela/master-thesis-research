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
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext context;

        public ReservationRepository(DataContext context)
        {
            this.context = context;
        }

        public void Delete(Reservation reservation)
        {
            context.Reservations.Remove(reservation);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await context.Reservations.Include(p => p.Table).ToListAsync();
        }

        public async Task<Reservation> GetAsync(Guid id)
        {
            return await context.Reservations.FirstOrDefaultAsync(r => r.Id == id); 
        }

        public async Task SaveAsync(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
        }

        public async Task<List<Reservation>> SearchByTableAsync(Guid tableId)
        {
            return await context.Reservations
                .Include(p => p.Table)
                .Where(r => r.TableId == tableId)
                .ToListAsync();
        }

        public void Update(Reservation reservation)
        {
            context.Reservations.Update(reservation);
        }
    }
}