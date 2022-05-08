using System;
using Domain.Models;

namespace Application.Resources.Products.Get
{
    public class ProductResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public Guid ProductsCategoryId { get; set; }
        public string ProductsCategoryName { get; set; }
    }
}