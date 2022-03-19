using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Orders.Get
{
    public class OrderedCourseForOrderResource
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public CourseForOrderResource Course {get; set;}

        [Required, Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int PercentageDiscount { get; set; }

        public List<StatusEntryForOrderResource> StatusEntries { get; set; }

    }
}