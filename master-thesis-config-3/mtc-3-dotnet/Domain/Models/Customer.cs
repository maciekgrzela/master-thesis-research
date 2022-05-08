using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string  Address2 { get; set; }
        [Required]
        public string NIP { get; set; }
    }
}