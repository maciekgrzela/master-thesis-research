using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Reservation.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<Response<List<Reservation>>> GetAllAsync();

        Task<Response<List<Reservation>>> SearchByTableAsync(Guid tableId);

        Task<Response<Reservation>> GetAsync(Guid id);

        Task<Response<Reservation>> SaveAsync(SaveReservationResource reservation);

        Task<Response<Reservation>> Update(Guid id, SaveReservationResource reservation);

        Task<Response<Reservation>> Delete(Guid id);
    }
}