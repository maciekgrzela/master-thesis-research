using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.Hall.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHallRepository hallRepository;
        public HallService(IHallRepository hallRepository, IUnitOfWork unitOfWork)
        {
            this.hallRepository = hallRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Hall>> Delete(Guid id)
        {
             var existingHall = await hallRepository.GetAsync(id);

            if (existingHall == null)
            {
                return new Response<Hall>(HttpStatusCode.NotFound, $"Hall with id:{id} not found");
            }

            hallRepository.Delete(existingHall);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Hall>(HttpStatusCode.NoContent, existingHall);
        }

        public async Task<Response<IEnumerable<Hall>>> GetAllAsync()
        {
            var halls = await hallRepository.GetAllAsync();
            return new Response<IEnumerable<Hall>>(HttpStatusCode.OK, halls);
        }

        public async Task<Response<Hall>> GetAsync(Guid id)
        {
            var hall = await hallRepository.GetAsync(id);

            if (hall == null)
            {
                return new Response<Hall>(HttpStatusCode.NotFound, $"Hall with id:{id} not found");
            }

            return new Response<Hall>(HttpStatusCode.OK, hall);
        }

        public async Task<Response<Hall>> SaveAsync(SaveHallResource hall)
        {
            var newHall = new Hall() {
                Id = Guid.NewGuid(),
                ColumnNumber = hall.ColumnNumber,
                RowNumber = hall.RowNumber,
                Description = hall.Description,
            };

            await hallRepository.SaveAsync(newHall);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Hall>(HttpStatusCode.NoContent, newHall);
        }

        public async Task<Response<Hall>> Update(Guid id, SaveHallResource hall)
        {
            var existingHall = await hallRepository.GetAsync(id);

            if(existingHall == null)
            {
                return new Response<Hall>(HttpStatusCode.NotFound, $"Hall with id:{id} not found");
            }

            existingHall.ColumnNumber = hall.ColumnNumber;
            existingHall.RowNumber = hall.RowNumber;
            existingHall.Description = hall.Description;

            hallRepository.Update(existingHall);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Hall>(HttpStatusCode.NoContent, existingHall);
        }
    }
}