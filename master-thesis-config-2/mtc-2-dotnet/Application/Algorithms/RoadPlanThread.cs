using System.Collections.Generic;
using System.Timers;
using Domain.Algorithms;
using Domain.Algorithms.Models;
using Coordinate = Domain.Algorithms.Coordinate;

namespace Application.Algorithms
{
    public class RoadPlanThread
    {
        public Road BestRoad;
        private static Timer _timer;
        private bool Timeout { get; set; }
        public List<Coordinate> Coordinates { get; set; } = new();
        public List<Domain.Algorithms.Models.Coordinate> BestCoordinates { get; set; }

        public int IterationsCount { get; private set; }

        public RoadPlanThread(IEnumerable<Domain.Algorithms.Models.Coordinate> coordinates)
        {
            BestRoad = null;
            BestCoordinates = new List<Domain.Algorithms.Models.Coordinate>();
            PrepareCoordinates(coordinates);
            Timeout = false;
            _timer = new Timer(60000);
            _timer.Elapsed += (sender, args) => Timeout = true;
            _timer.AutoReset = false;
        }

        private void PrepareCoordinates(IEnumerable<Domain.Algorithms.Models.Coordinate> coordinates)
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
            var population = Population.Randomized(startSolution, Config.populationSize);
            var better = true;
            var iterations = 0;

            _timer.Start();
            
            while (!Timeout)
            {
                if (better)
                    SetBestRoad(population);
            
                better = false;
                var oldFit = population.MaxFitness;
            
                population = population.Evolve();
                if (population.MaxFitness > oldFit)
                    better = true;

                iterations++;
            }

            IterationsCount = iterations;
            PrepareBestRoadCoords();
        }

        private void PrepareBestRoadCoords()
        {
            var i = 1;
            if (BestRoad == null)
            {
                return;
            }
            
            foreach (var coord in BestRoad.Coordinates)
            {
                BestCoordinates.Add
                (
                    new Domain.Algorithms.Models.Coordinate 
                    {
                        Latitude = coord.Latitude,
                        Longitude = coord.Longitude,
                        Address = coord.Address,
                        Order = i
                    }
                );
                i++;
            }
        }

        private void SetBestRoad(Population p)
        {
            var best = p.FindBest();
            BestRoad = best;
        }
    }
}