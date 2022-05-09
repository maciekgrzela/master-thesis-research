using System;

namespace Application.Resources.Reservation.Get
{
    public class ReservationResource
    {
        public Guid Id {get; set;}

        public Guid TableId { get; set; }

        public DateTime Beginning { get; set; }

        public DateTime? Ending { get; set; }
    }
}