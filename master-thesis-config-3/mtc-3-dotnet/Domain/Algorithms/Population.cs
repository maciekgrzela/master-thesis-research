using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Algorithms.Models;

namespace Domain.Algorithms
{
    public class Population
    {
        public List<Road> p { get; private set; }
        public double maxFit { get; private set; }
        public static Random randomGenerator { get; set; }

        public Population(List<Road> l)
        {
            p = l;
            maxFit = this.calcMaxFit();
            randomGenerator = new Random();
        }

        public static Population randomized(Road t, int n)
        {
            List<Road> tmp = new List<Road>();

            for (int i = 0; i < n; ++i)
                tmp.Add( t.rearrange() );

            return new Population(tmp);
        }

        private double calcMaxFit() => p.Max( t => t.FitnessRatio );

        public Road select()
        {
            while (true)
            {
                int i = randomGenerator.Next(0, Config.populationSize);

                if (randomGenerator.NextDouble() < p[i].FitnessRatio / maxFit)
                    return new Road(p[i].Coordinates);
            }
        }

        public Population genNewPop(int n)
        {
            List<Road> p = new List<Road>();

            for (int i = 0; i < n; ++i)
            {
                Road t = select().performCrossing( select() );

                foreach (Coordinate c in t.Coordinates)
                    t = t.performMutation();

                p.Add(t);
            }

            return new Population(p);
        }

        public Population elite(int n)
        {
            List<Road> best = new List<Road>();
            Population tmp = new Population(p);

            for (int i = 0; i < n; ++i)
            {
                best.Add( tmp.findBest() );
                tmp = new Population( tmp.p.Except(best).ToList() );
            }

            return new Population(best);
        }

        public Road findBest()
        {
            foreach (Road t in this.p)
            {
                if (t.FitnessRatio == this.maxFit)
                    return t;
            }

            return null;
        }

        public Population evolve()
        {
            var best = elite(Config.numberOfDominantsInNextGeneration);
            var np = genNewPop(Config.populationSize - Config.numberOfDominantsInNextGeneration);
            return new Population( best.p.Concat(np.p).ToList() );
        }
    }
}