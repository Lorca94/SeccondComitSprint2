namespace ForumBackEnd.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        

        // Relacion con User
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Relación con Module
        public virtual List<Module> Modules { get; set; }
    }
}
