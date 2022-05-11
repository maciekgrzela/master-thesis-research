using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}