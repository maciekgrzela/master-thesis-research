using System;
using System.Collections.Generic;
using Domain.Models;

namespace Application.Resources.Course.Get
{
    public class CourseResource
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double GrossPrice { get; set; }
        public double NetPrice { get; set; }
        public int Tax { get; set; }
        public int PreparationTimeInMinutes { get; set; }
        public Guid CoursesCategoryId { get; set; }
        public string CoursesCategoryName { get; set; }
    }
}