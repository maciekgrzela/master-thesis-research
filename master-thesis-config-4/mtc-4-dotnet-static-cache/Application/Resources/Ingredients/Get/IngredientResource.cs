using System;
using Application.Resources.Products.Get;

namespace Application.Resources.Ingredients.Get
{
    public class IngredientResource
    {
        public Guid Id { get; set; }
        public ProductResource Product { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}