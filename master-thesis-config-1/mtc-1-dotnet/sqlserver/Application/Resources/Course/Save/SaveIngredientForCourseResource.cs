using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Course.Save
{
    public class SaveIngredientForCourseResource
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}