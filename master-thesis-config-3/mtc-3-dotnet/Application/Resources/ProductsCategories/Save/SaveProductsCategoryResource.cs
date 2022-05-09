using System.ComponentModel.DataAnnotations;

namespace Application.Resources.ProductsCategories.Save
{
    public class SaveProductsCategoryResource
    {
        [Required]
        public string Name { get; set; }
    }
}