using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Ingredient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}