using Application.Resources.Statuses.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly IStatusesRepository statusesRepository;
        private readonly IUnitOfWork unitOfWork;

        public StatusesService(IStatusesRepository statusesRepository, IUnitOfWork unitOfWork)
        {
            this.statusesRepository = statusesRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<Status>> GetStatusAsync(Guid id)
        {
            var existingStatus = await statusesRepository.GetStatusAsync(id);

            if (existingStatus == null)
            {
                return new Response<Status>(HttpStatusCode.NotFound, $"Status with id:{id} not found");
            }

            return new Response<Status>(HttpStatusCode.OK, existingStatus);
        }

        public async Task<Response<IEnumerable<Status>>> ListAsync()
        {
            var statuses = await statusesRepository.ListAsync();
            return new Response<IEnumerable<Status>>(HttpStatusCode.OK, statuses);
        }

        public async Task<Response<Status>> SaveAsync(SaveStatusResource status)
        {
            var savedStatusId = Guid.NewGuid();

            var savedStatus = new Status
            {
                Id = savedStatusId,
                Name = status.Name,
            };

            await statusesRepository.SaveAsync(savedStatus);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Status>(HttpStatusCode.NoContent, savedStatus);
        }

        public async Task<Response<Status>> UpdateAsync(Guid id, SaveStatusResource status)
        {
            var existingStatus = await statusesRepository.GetStatusAsync(id);

            if (existingStatus == null)
            {
                return new Response<Status>(HttpStatusCode.NotFound, $"Status with id:{id} not found");
            }

            existingStatus.Name = status.Name;

            statusesRepository.Update(existingStatus);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Status>(HttpStatusCode.NoContent, existingStatus);
        }

        public async Task<Response<Status>> DeleteAsync(Guid id)
        {
            var existingStatus = await statusesRepository.GetStatusAsync(id);

            if (existingStatus == null)
            {
                return new Response<Status>(HttpStatusCode.NotFound, $"Status with id:{id} not found");
            }

            statusesRepository.Delete(existingStatus);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Status>(HttpStatusCode.NoContent, existingStatus);
        }
    }
}
