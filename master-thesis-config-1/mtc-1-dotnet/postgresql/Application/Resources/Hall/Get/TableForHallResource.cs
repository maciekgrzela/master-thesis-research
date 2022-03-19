using System;

namespace Application.Resources.Hall.Get
{
    public class TableForHallResource
    {
        public Guid Id {get; set;}
		public int StartCoordinateX { get; set; }
		public int StartCoordinateY { get; set; }
		public int EndCoordinateX { get; set; }
		public int EndCoordinateY { get; set; }
    }
}