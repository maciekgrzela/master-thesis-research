using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.Reservation.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReservationRepository reservationRepository;
        private readonly ITableRepository tableRepository;

        public ReservationService(IReservationRepository reservationRepository, ITableRepository tableRepository, IUnitOfWork unitOfWork)
        {
            this.reservationRepository = reservationRepository;
            this.tableRepository = tableRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Reservation>> Delete(Guid id)
        {
            var existingReservation = await reservationRepository.GetAsync(id);

            if (existingReservation == null)
            {
                return new Response<Reservation>(HttpStatusCode.NotFound, $"Reservation with id:{id} not found");
            }

            reservationRepository.Delete(existingReservation);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Reservation>(HttpStatusCode.NoContent, existingReservation);
        }

        public async Task<Response<IEnumerable<Reservation>>> GetAllAsync()
        {
            var reservations = await reservationRepository.GetAllAsync();
            return new Response<IEnumerable<Reservation>>(HttpStatusCode.OK, reservations);
        }

        public async Task<Response<Reservation>> GetAsync(Guid id)
        {
            var reservation = await reservationRepository.GetAsync(id);

            if (reservation == null)
            {
                return new Response<Reservation>(HttpStatusCode.NotFound, $"Reservation with id:{id} not found");
            }

            return new Response<Reservation>(HttpStatusCode.OK, reservation);
        }

        public async Task<Response<Reservation>> SaveAsync(SaveReservationResource reservation)
        {
            var table = await tableRepository.GetAsync(reservation.TableId);

            if (table == null)
            {
                return new Response<Reservation>(HttpStatusCode.NotFound, $"Table with id:{reservation.TableId} not found");
            }

            var newReservation = new Reservation() {
                Id = Guid.NewGuid(),
                Beginning = reservation.Beginning,
                Ending = reservation.Ending,
                TableId = reservation.TableId
            };

            await reservationRepository.SaveAsync(newReservation);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Reservation>(HttpStatusCode.NoContent, newReservation);
        }

        public async Task<Response<IEnumerable<Reservation>>> SearchByTableAsync(Guid tableId)
        {
            var reservations = await reservationRepository.SearchByTableAsync(tableId);
            return new Response<IEnumerable<Reservation>>(HttpStatusCode.OK, reservations);
        }

        public async Task<Response<Reservation>> Update(Guid id, SaveReservationResource reservation)
        {
            var existingReservation = await reservationRepository.GetAsync(id);

            if(existingReservation == null)
            {
                return new Response<Reservation>(HttpStatusCode.NotFound, $"Reservation with id:{id} not found");
            }

            var table = await tableRepository.GetAsync(reservation.TableId);

            if (table == null)
            {
                return new Response<Reservation>(HttpStatusCode.NotFound, $"Table with id:{reservation.TableId} not found");
            }

            existingReservation.Beginning = reservation.Beginning;
            existingReservation.Ending = reservation.Ending;
            existingReservation.TableId = reservation.TableId;

            reservationRepository.Update(existingReservation);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Reservation>(HttpStatusCode.NoContent, existingReservation);
        }
    }
}