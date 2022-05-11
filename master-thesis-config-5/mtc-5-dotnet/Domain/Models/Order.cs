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
        public virtual IEnumerable<OrderedCourse> OrderedCourses { get; set; }
		public virtual IEnumerable<Bill> Bills { get; set; }
        public virtual IEnumerable<StatusEntry> StatusEntries { get; set; }
        [MaxLength(1000)]
        public string Note { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
    }
}