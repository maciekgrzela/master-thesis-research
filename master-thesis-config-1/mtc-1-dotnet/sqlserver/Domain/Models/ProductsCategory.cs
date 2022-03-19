using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductsCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}