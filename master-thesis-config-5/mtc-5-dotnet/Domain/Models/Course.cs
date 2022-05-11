using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(500)]
        public string Name { get; set; }
        
        [Range(0, double.MaxValue)]
        public double GrossPrice { get; set; }
        
        [Range(0, double.MaxValue)]
        public double NetPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Tax { get; set; }
        
        [Range(0, 86400)]
        public int PreparationTimeInMinutes { get; set; }
        public Guid CoursesCategoryId { get; set; }
        public virtual CoursesCategory CoursesCategory { get; set; }
        public virtual IEnumerable<Ingredient> Ingredients { get; set; }
    }
}