using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Address1 { get; set; }
        [MaxLength(1000)]
        public string  Address2 { get; set; }
        
        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string NIP { get; set; }
    }
}