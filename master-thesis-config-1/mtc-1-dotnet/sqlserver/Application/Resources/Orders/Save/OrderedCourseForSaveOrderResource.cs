using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Orders.Save
{
    public class OrderedCourseForSaveOrderResource
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int PercentageDiscount { get; set; }
    }
}