using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace Domain.Algorithms
{
    [NotMapped]
    public class Coordinate
    {
        public int Order { get; set; } = 0;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }

        public Coordinate(double latitude, double longitude, string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }

        public double distance(Coordinate coordinate) =>
            Math.Sqrt(Math.Pow(coordinate.Latitude - Latitude, 2) + Math.Pow(coordinate.Longitude - Longitude, 2));

        public override string ToString()
        {
            return $"({Latitude}, {Longitude})";
        }
    }
}