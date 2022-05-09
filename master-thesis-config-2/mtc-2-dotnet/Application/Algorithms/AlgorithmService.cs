using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Algorithms.Interfaces;
using Application.Resources.Algorithms;
using Application.Responses;
using Domain.Algorithms.Models;

namespace Application.Algorithms
{
    public class AlgorithmService : IAlgorithmService
    {

        public async Task<Response<RoadPlanResult>> RoadPlan(List<Coordinate> coordinates, double bestResult)
        {
            Config.mutationProbability = 0.01;
            Config.populationSize = Convert.ToInt32(Math.Floor(0.75*Convert.ToDouble(coordinates.Count)));
            Config.numberOfCoordinates = coordinates.Count;
            Config.numberOfDominantsInNextGeneration =
                Convert.ToInt32(Math.Floor(0.25 * Convert.ToDouble(coordinates.Count)));
            
            var roadPlanThread = new RoadPlanThread(coordinates);

            var thread = new Thread(roadPlanThread.Run);
            
            thread.Start();
            thread.Join();
            
            return roadPlanThread.IterationsCount == 0 || roadPlanThread.BestRoad.Distance == 0.0 ?
                new Response<RoadPlanResult>(HttpStatusCode.InternalServerError, "Could not determine the most optimal road") :
                new Response<RoadPlanResult>(HttpStatusCode.OK, new RoadPlanResult
                {
                    ResultToBestRatio = bestResult / roadPlanThread.BestRoad.Distance,
                    IterationsCount = roadPlanThread.IterationsCount
                });
        }
    }
}