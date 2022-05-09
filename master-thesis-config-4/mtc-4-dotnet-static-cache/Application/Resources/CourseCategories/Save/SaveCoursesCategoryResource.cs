using System.ComponentModel.DataAnnotations;

namespace Application.Resources.CourseCategories.Save
{
    public class SaveCoursesCategoryResource
    {
        [Required]
        public string Name { get; set; }
    }
}