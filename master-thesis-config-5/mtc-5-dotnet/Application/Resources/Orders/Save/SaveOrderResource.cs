using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Resources.OrderedCourses.Save;

namespace Application.Resources.Orders.Save
{
    public class SaveOrderResource
    {
        public Guid TableId {get; set;}
        public string Note { get; set; }
        public List<OrderedCourseForSaveOrderResource> OrderedCourses { get; set; }
    }
}