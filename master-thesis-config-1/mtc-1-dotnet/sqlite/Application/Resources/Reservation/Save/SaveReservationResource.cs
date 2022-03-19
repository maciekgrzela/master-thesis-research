using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Reservation.Save
{
    public class SaveReservationResource
    {
        [Required]
        public Guid TableId { get; set; }

        [Required]
        public DateTime Beginning { get; set; }

        [Required]
        public DateTime? Ending { get; set; }
    }
}