using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Table
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public Guid HallId { get; set; }

		public Hall Hall { get; set; }
		
		public List<Order> Orders { get; set; }

		public List<Reservation> Reservations { get; set; }

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
