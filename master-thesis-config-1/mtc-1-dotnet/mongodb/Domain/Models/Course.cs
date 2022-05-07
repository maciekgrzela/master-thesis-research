using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double GrossPrice { get; set; }
        public double NetPrice { get; set; }
        public int Tax { get; set; }
        public int PreparationTimeInMinutes { get; set; }
        public Guid CoursesCategoryId { get; set; }
        public virtual CoursesCategory CoursesCategory { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}