using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public Guid ProductsCategoryId { get; set; }
        public virtual ProductsCategory ProductsCategory { get; set; }
    }
}