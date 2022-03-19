using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Resources.Table.Save;
using Application.Responses;
using Application.Services.Interfaces;
using Domain.Models;
using Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITableRepository tableRepository;
        private readonly IHallRepository hallRepository;

        public TableService(ITableRepository tableRepository, IHallRepository hallRepository, IUnitOfWork unitOfWork)
        {
            this.tableRepository = tableRepository;
            this.hallRepository = hallRepository;
            this.unitOfWork = unitOfWork;
        }
        
        public async Task<Response<Table>> Delete(Guid id)
        {
            var existingTable = await tableRepository.GetAsync(id);

            if (existingTable == null)
            {
                return new Response<Table>(HttpStatusCode.NotFound, $"Table with id:{id} not found");
            }

            tableRepository.Delete(existingTable);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Table>(HttpStatusCode.NoContent, existingTable);
        }

        public async Task<Response<List<Table>>> GetAllAsync()
        {
            var tables = await tableRepository.GetAllAsync();
            return new Response<List<Table>>(HttpStatusCode.OK, tables);
        }

        public async Task<Response<List<Table>>> GetAllForHallAsync(Guid hallId)
        {
            var tables = await tableRepository.GetAllForHallAsync(hallId);
            return new Response<List<Table>>(HttpStatusCode.OK, tables);
        }

        public async Task<Response<Table>> GetAsync(Guid id)
        {
            var table = await tableRepository.GetAsync(id);

            if (table == null)
            {
                return new Response<Table>(HttpStatusCode.NotFound, $"Table with id:{id} not found");
            }

            return new Response<Table>(HttpStatusCode.OK, table);
        }

        public async Task<Response<Table>> SaveAsync(SaveTableResource table)
        {
            var hall = await hallRepository.GetAsync(table.HallId);

            if (hall == null)
            {
                return new Response<Table>(HttpStatusCode.NotFound, $"Hall with id:{table.HallId} not found");
            }

            var newTable = new Table() {
                Id = Guid.NewGuid(),
                StartCoordinateX = table.StartCoordinateX,
                EndCoordinateX = table.EndCoordinateX,
                StartCoordinateY = table.StartCoordinateY,
                EndCoordinateY = table.EndCoordinateY,
                HallId = table.HallId,
            };

            await tableRepository.SaveAsync(newTable);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Table>(HttpStatusCode.NoContent, newTable);
        }

        public async Task<Response<Table>> Update(Guid id, SaveTableResource table)
        {
            var existingTable = await tableRepository.GetAsync(id);

            if(existingTable == null)
            {
                return new Response<Table>(HttpStatusCode.NotFound, $"Table with id:{id} not found");
            }

            var hall = await hallRepository.GetAsync(table.HallId);

            if (hall == null)
            {
                return new Response<Table>(HttpStatusCode.NotFound, $"Hall with id:{table.HallId} not found");
            }

            existingTable.StartCoordinateX = table.StartCoordinateX;
            existingTable.EndCoordinateX = table.EndCoordinateX;
            existingTable.StartCoordinateY = table.StartCoordinateY;
            existingTable.EndCoordinateY = table.EndCoordinateY;
            existingTable.HallId = table.HallId;

            tableRepository.Update(existingTable);
            await unitOfWork.CommitTransactionAsync();

            return new Response<Table>(HttpStatusCode.NoContent, existingTable);
        }
    }
}