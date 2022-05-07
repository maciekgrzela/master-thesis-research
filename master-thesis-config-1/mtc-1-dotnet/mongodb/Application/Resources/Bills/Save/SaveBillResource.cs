using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Resources.OrderedCourses.Get;

namespace Application.Resources.Bills.Save
{
    public class SaveBillResource
    {
        public Guid? CustomerId { get; set; }
        public string MongoCustomerId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public int Tax { get; set; }
        public List<OrderedCourseForNewBillResource> OrderedCourses { get; set; }
    }
}