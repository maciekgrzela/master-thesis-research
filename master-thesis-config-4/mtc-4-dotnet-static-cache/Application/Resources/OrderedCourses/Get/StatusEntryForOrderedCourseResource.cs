using System;

namespace Application.Resources.OrderedCourses.Get
{
    public class StatusEntryForOrderedCourseResource
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
    }
}