using System.ComponentModel.DataAnnotations;

namespace ForumBackEnd.DTO
{
    public class CourseDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
