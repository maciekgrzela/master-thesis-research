using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public virtual List<OrderedCourse> OrderedCourses { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public double NetPrice { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public double Tax { get; set; }
        [Required]
        public double GrossPrice => NetPrice + ((0.01 * Tax) * NetPrice);
        public DateTime Created { get; set; }
    }
}