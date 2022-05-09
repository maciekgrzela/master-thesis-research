using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrderedCourse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid? BillId { get; set; }
        public virtual Bill Bill { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, Int32.MaxValue)]
        public int? BillQuantity { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int PercentageDiscount { get; set; }
        public virtual List<StatusEntry> StatusEntries { get; set; }
    }
}