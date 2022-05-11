using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Hall.Save;
using Application.Responses;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IHallService
    {
        Task<Response<IEnumerable<Hall>>> GetAllAsync();

        Task<Response<Hall>> GetAsync(Guid id);

        Task<Response<Hall>> SaveAsync(SaveHallResource hall);

        Task<Response<Hall>> Update(Guid id, SaveHallResource hall);

        Task<Response<Hall>> Delete(Guid id);
    }
}