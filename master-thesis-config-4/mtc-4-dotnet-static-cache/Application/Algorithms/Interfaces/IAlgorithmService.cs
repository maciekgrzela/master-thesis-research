using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses;
using Domain.Algorithms.Models;

namespace Application.Algorithms.Interfaces
{
    public interface IAlgorithmService
    {
        Task<Response<List<Coordinate>>> RoadPlan(List<Coordinate> coordinates);
    }
}