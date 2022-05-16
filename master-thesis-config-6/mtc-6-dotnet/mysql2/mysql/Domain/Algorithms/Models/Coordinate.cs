using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Domain.Models;

namespace Domain.Algorithms.Models
{
    [NotMapped]
    public class Coordinate
    {
        public int Order { get; set; } = 0;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}