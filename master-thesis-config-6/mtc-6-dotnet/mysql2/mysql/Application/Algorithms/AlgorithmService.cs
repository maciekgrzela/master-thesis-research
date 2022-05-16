using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Algorithms.Interfaces;
using Application.Responses;
using Domain.Algorithms.Models;

namespace Application.Algorithms
{
    public class AlgorithmService : IAlgorithmService
    {

        public async Task<Response<List<Coordinate>>> RoadPlan(List<Coordinate> coordinates)
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
            
            return roadPlanThread.BestCoordinates.Count == 0 ?
                new Response<List<Coordinate>>(HttpStatusCode.InternalServerError, "Could not determine the most optimal road") :
                new Response<List<Coordinate>>(HttpStatusCode.OK, roadPlanThread.BestCoordinates);
        }
    }
}