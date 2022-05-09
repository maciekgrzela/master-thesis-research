using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Algorithms.Models
{
    [NotMapped]
    public static class Config
    {
        public static double mutationProbability;
        public static int numberOfCoordinates;
        public static int numberOfDominantsInNextGeneration;
        public static int populationSize;
    }
}