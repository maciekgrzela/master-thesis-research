using System.Collections.Generic;
using System.Timers;
using Domain.Algorithms;
using Domain.Algorithms.Models;
using Coordinate = Domain.Algorithms.Coordinate;

namespace Application.Algorithms
{
    public class RoadPlanThread
    {
        private Road bestRoad;
        private static Timer _timer;
        private bool Timeout { get; set; }
        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
        public List<Domain.Algorithms.Models.Coordinate> BestCoordinates { get; set; }

        public RoadPlanThread(List<Domain.Algorithms.Models.Coordinate> coordinates)
        {
            bestRoad = null;
            BestCoordinates = new List<Domain.Algorithms.Models.Coordinate>();
            prepareCoordinates(coordinates);
            Timeout = false;
            _timer = new Timer(60000);
            _timer.Elapsed += (sender, args) => Timeout = true;
            _timer.AutoReset = false;
        }

        private void prepareCoordinates(List<Domain.Algorithms.Models.Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                var coord = new Coordinate(coordinate.Latitude, coordinate.Longitude, coordinate.Address);
                Coordinates.Add(coord);
            }
        }

        public void Run()
        {
            var startSolution = new Road(Coordinates);
            var population = Population.randomized(startSolution, Config.populationSize);
            var better = true;
            
            _timer.Start();
            
            while (!Timeout)
            {
                if (better)
                    SetBestRoad(population);
            
                better = false;
                var oldFit = population.maxFit;
            
                population = population.evolve();
                if (population.maxFit > oldFit)
                    better = true;
            }

            prepareBestRoadCoords();
        }

        private void prepareBestRoadCoords()
        {
            var i = 1;
            if (bestRoad == null)
            {
                return;
            }
            
            foreach (var coord in bestRoad.Coordinates)
            {
                var bestCoord = new Domain.Algorithms.Models.Coordinate();
                bestCoord.Latitude = coord.Latitude;
                bestCoord.Longitude = coord.Longitude;
                bestCoord.Address = coord.Address;
                bestCoord.Order = i;
                BestCoordinates.Add(bestCoord);
                i++;
            }
        }

        private void SetBestRoad(Population p)
        {
            var best = p.findBest();
            bestRoad = best;
        }
    }
}