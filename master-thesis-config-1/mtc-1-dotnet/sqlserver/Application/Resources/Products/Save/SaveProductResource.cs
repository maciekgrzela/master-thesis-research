using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Products.Save
{
    public class SaveProductResource
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public Guid ProductsCategoryId { get; set; }
    }
}