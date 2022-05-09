using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Resources.Course.Save
{
    public class SaveCourseResource
    {
        public string Name { get; set; }
        public double GrossPrice { get; set; }
        public double NetPrice { get; set; }
        public int Tax { get; set; }
        public int PreparationTimeInMinutes { get; set; }
        public Guid CoursesCategoryId { get; set; }
        public List<SaveIngredientForCourseResource> Ingredients { get; set; }
    }
}
