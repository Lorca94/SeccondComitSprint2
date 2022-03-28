using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual List<CourseUser>? ModuleRelations { get; set; }
        public virtual List<Question>? Questions { get; set; }
    }
}
