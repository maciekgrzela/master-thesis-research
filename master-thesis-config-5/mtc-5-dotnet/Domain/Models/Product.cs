using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        
        [MaxLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string Unit { get; set; }
        public Guid ProductsCategoryId { get; set; }
        public virtual ProductsCategory ProductsCategory { get; set; }
    }
}