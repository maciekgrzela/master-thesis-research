using System;

namespace Application.Resources.Course.Get
{
    public class ProductForIngredientForCourseResource
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public Guid ProductsCategoryId { get; set; }
        public string ProductsCategoryName { get; set; }
    }
}