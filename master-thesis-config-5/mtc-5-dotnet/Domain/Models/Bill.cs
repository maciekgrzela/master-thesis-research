using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual IEnumerable<OrderedCourse> OrderedCourses { get; set; }
        
        [Required, Range(0, Double.MaxValue)]
        public double NetPrice { get; set; }
        
        [Required, Range(0, Double.MaxValue)]
        public double Tax { get; set; }
        
        [Required]
        public double GrossPrice { get; set; }
        public DateTime Created { get; set; }
    }
}