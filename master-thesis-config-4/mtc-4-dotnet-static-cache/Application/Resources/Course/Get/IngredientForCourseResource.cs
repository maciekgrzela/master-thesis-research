using System;

namespace Application.Resources.Course.Get
{
    public class IngredientForCourseResource
    {
        public Guid Id { get; set; }
        public virtual ProductForIngredientForCourseResource Product { get; set; }
        public double Amount { get; set; }
    }
}