using System;
using System.Collections.Generic;
using Application.Resources.Bills.Get;
using Application.Resources.Course.Get;
using Application.Resources.Orders.Get;
using Application.Resources.StatusEntries.Get;

namespace Application.Resources.OrderedCourses.Get
{
    public class OrderedCourseResource
    {
        public Guid Id { get; set; }
        public CourseResource Course { get; set; }
        public Guid OrderId { get; set; }
        public Guid? BillId { get; set; }
        public int Quantity { get; set; }
        public double NetTotal { get; set; }
        public double GrossTotal { get; set; }
        public int PercentageDiscount { get; set; }
        public List<StatusEntryForOrderedCourseResource> StatusEntries { get; set; }
    }
}