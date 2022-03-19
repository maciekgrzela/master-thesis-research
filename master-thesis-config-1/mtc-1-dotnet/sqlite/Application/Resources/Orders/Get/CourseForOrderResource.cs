using System;

namespace Application.Resources.Orders.Get
{
    public class CourseForOrderResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double GrossPrice { get; set; }
        public double NetPrice { get; set; }
        public int Tax { get; set; }

    }
}