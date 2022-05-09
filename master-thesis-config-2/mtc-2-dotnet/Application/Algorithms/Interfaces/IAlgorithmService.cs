using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Resources.Algorithms;
using Application.Responses;
using Domain.Algorithms.Models;

namespace Application.Algorithms.Interfaces
{
    public interface IAlgorithmService
    {
        Task<Response<RoadPlanResult>> RoadPlan(List<Coordinate> coordinates, double bestResult);
    }
}