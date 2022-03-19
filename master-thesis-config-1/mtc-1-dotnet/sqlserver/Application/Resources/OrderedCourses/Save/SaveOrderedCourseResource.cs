using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.OrderedCourses.Save
{
    public class SaveOrderedCourseResource
    {
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        public Guid? BillId { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, Int32.MaxValue)]
        public int? BillQuantity { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int PercentageDiscount { get; set; }
    }
}