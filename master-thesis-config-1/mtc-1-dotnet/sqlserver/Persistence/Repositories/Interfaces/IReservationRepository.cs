using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync();

        Task<List<Reservation>> SearchByTableAsync(Guid tableId);

        Task<Reservation> GetAsync(Guid id);

        Task SaveAsync(Reservation reservation);

        void Update(Reservation reservation);

        void Delete(Reservation reservation);
    }
}