using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Algorithms.Models;

namespace Domain.Algorithms
{
    public class Road
    {
        public double Distance { get; set; } = 0.0;
        public double FitnessRatio { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        
        public static Random randomGenerator { get; set; }


        public Road(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
            prepareRoadParams();
            randomGenerator = new Random();
        }

        private void prepareRoadParams()
        {
            Distance = calculateDistance();
            FitnessRatio = calculateFitnessRatio();
        }

        private double calculateDistance()
        {
            double totalDistance = 0.0;
            for (int i = 0; i < Coordinates.Count; i++)
            {
                var nextItemIndex = (i + 1) % Coordinates.Count;
                totalDistance += Coordinates[i].distance(Coordinates[nextItemIndex]);
            }

            return totalDistance;
        }

        private double calculateFitnessRatio()
        {
            if (Distance == 0)
            {
                Distance = calculateDistance();
            }

            return 1.0 / Distance;
        }
        
        public override string ToString()
        {
            string roadString = String.Empty;
            for (int i = 0; i < Coordinates.Count; i++)
            {
                roadString += $"{Coordinates[i]} -> ";
            }
            roadString += $"{Coordinates[0]}";
            return roadString;
        }

        public Road PerformMutation()
        {
            var coords = new List<Coordinate>(Coordinates);
            var prob = randomGenerator.NextDouble();
            Road road = null;

            if (Config.mutationProbability > prob)
            {
                int swappedIndexOne = randomGenerator.Next(0, Coordinates.Count);
                int swappedIndexTwo = randomGenerator.Next(0, Coordinates.Count);

                var temp = coords[swappedIndexOne];
                coords[swappedIndexOne] = coords[swappedIndexTwo];
                coords[swappedIndexTwo] = temp;
            }
            
            road = new Road(coords);
            
            return road;
        }

        public Road PerformCrossing(Road road)
        {
            int i = randomGenerator.Next(0, road.Coordinates.Count);
            int j = randomGenerator.Next(i, road.Coordinates.Count);
            Road returnedRoad = null;
            
            List<Coordinate> s = Coordinates.GetRange(i, j - i + 1);
            List<Coordinate> ms = road.Coordinates.Except(s).ToList();
            List<Coordinate> c = ms.Take(i)
                .Concat(s)
                .Concat( ms.Skip(i) )
                .ToList();
            
            returnedRoad = new Road(c);
            
            return returnedRoad;
        }

        public Road Rearrange()
        {
            List<Coordinate> tmp = new List<Coordinate>(Coordinates);
            int n = tmp.Count;

            while (n > 1)
            {
                n--;
                var k = randomGenerator.Next( n + 1);
                var v = tmp[k];
                tmp[k] = tmp[n];
                tmp[n] = v;
            }

            return new Road(tmp);
        }
    }
}