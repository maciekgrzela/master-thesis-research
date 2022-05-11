using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IHallRepository
    {
        Task<IEnumerable<Hall>> GetAllAsync();

        Task<Hall> GetAsync(Guid id);

        Task SaveAsync(Hall hall);

        void Update(Hall hall);

        void Delete(Hall hall);
    }
}