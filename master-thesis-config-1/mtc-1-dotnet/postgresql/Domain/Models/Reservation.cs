using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TableId { get; set; }

        public Table Table { get; set; }

        [Required]
        public DateTime Beginning { get; set; }

        public DateTime? Ending { get; set; }
    }
}
