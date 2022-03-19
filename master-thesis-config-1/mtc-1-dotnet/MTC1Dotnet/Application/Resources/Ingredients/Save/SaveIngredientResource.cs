using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Ingredients.Save
{
    public class SaveIngredientResource
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}