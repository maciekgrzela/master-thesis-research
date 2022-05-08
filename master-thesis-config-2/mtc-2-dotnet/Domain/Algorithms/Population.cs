using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Algorithms.Models;

namespace Domain.Algorithms
{
    public class Population
    {
        public List<Road> Roads { get; private set; }
        public double MaxFitness { get; private set; }
        public static Random RandomGenerator { get; set; }

        public Population(List<Road> roads)
        {
            Roads = roads;
            MaxFitness = CalculateMaxFitness();
            RandomGenerator = new Random();
        }

        public static Population Randomized(Road road, int size)
        {
            var tmp = new List<Road>();

            for (var i = 0; i < size; ++i)
                tmp.Add( road.Rearrange() );

            return new Population(tmp);
        }

        private double CalculateMaxFitness() => Roads.Max( t => t.FitnessRatio );

        public Road Selection()
        {
            while (true)
            {
                var index = RandomGenerator.Next(0, Config.populationSize);

                if (RandomGenerator.NextDouble() < Roads[index].FitnessRatio / MaxFitness)
                    return new Road(Roads[index].Coordinates);
            }
        }

        public Population GenerateNewPopulation(int size)
        {
            var roads = new List<Road>();

            for (var i = 0; i < size; ++i)
            {
                var road = Selection().PerformCrossing(Selection());

                foreach (var coord in road.Coordinates)
                    road = road.PerformMutation();

                roads.Add(road);
            }

            return new Population(roads);
        }

        public Population GetEliteIndividuals(int size)
        {
            var roads = new List<Road>();
            var tmp = new Population(Roads);

            for (var i = 0; i < size; ++i)
            {
                roads.Add( tmp.FindBest() );
                tmp = new Population( tmp.Roads.Except(roads).ToList() );
            }

            return new Population(roads);
        }

        public Road FindBest()
        {
            return Roads.FirstOrDefault(t => t.FitnessRatio.CompareTo(MaxFitness) == 0);
        }

        public Population Evolve()
        {
            var elite = GetEliteIndividuals(Config.numberOfDominantsInNextGeneration);
            var newPopulation = GenerateNewPopulation(Config.populationSize - Config.numberOfDominantsInNextGeneration);
            return new Population( elite.Roads.Concat(newPopulation.Roads).ToList() );
        }
    }
}