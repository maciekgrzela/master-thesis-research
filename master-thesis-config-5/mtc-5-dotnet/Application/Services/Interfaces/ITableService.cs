using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Table.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITableService
    {
        Task<Response<IEnumerable<Table>>> GetAllAsync();

        Task<Response<IEnumerable<Table>>> GetAllForHallAsync(Guid hallId);

        Task<Response<Table>> GetAsync(Guid id);

        Task<Response<Table>> SaveAsync(SaveTableResource table);

        Task<Response<Table>> Update(Guid id, SaveTableResource table);

        Task<Response<Table>> Delete(Guid id);
    }
}