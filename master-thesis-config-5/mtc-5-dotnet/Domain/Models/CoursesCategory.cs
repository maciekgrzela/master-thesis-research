using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class CoursesCategory
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string Name { get; set; }
    }
}