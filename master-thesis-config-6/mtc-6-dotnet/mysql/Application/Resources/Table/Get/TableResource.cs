using System;

namespace Application.Resources.Table.Get
{
    public class TableResource
    {
        public Guid Id {get; set;}

		public Guid HallId { get; set; }

		public int StartCoordinateX { get; set; }
		
		public int StartCoordinateY { get; set; }

		public int EndCoordinateX { get; set; }

		public int EndCoordinateY { get; set; }
    }
}