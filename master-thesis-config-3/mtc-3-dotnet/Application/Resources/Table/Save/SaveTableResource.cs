using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Table.Save
{
    public class SaveTableResource
    {
        [Required]
		public Guid HallId { get; set; }

		[Required, Range(0, Int32.MaxValue)]
		public int StartCoordinateX { get; set; }
		
		[Required, Range(0, Int32.MaxValue)]
		public int StartCoordinateY { get; set; }

		[Required, Range(0, Int32.MaxValue)]
		public int EndCoordinateX { get; set; }

		[Required, Range(0, Int32.MaxValue)]
		public int EndCoordinateY { get; set; }
    }
}