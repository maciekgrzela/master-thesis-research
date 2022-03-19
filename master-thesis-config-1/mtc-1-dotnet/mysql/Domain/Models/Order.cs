using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public virtual Table Table { get; set; }
        public List<OrderedCourse> OrderedCourses { get; set; }
		public List<Bill> Bills { get; set; }
        public List<StatusEntry> StatusEntries { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}