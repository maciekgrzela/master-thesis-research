using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class StatusEntry
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
        public string Note { get; set; }
        public Guid? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid? OrderedCourseId { get; set; }
        public virtual OrderedCourse OrderedCourse { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}