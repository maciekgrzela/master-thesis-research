using System;
using System.Collections.Generic;
using Domain.Models;

namespace Application.Resources.Orders.Get
{
    public class OrderResource
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public List<OrderedCourseForOrderResource> OrderedCourses { get; set; }
        public List<StatusEntryForOrderResource> StatusEntries { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
    }
}