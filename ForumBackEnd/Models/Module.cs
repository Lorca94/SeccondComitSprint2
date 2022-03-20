namespace ForumBackEnd.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Relación con user
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Relación con Course
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        // Relacion con Question
        public virtual List<Question>? Questions { get; set; }
        
    }
}
