using Microsoft.AspNetCore.Identity;

namespace ForumBackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        // Relación con role
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        // Relacion con Course
        public virtual List<Course>? Courses { get; set; }
        
        // Relacion con Module
        public virtual List<Module>? Modules { get; set; }
        
        // Relacion con Question
        public virtual List<Question>? Questions { get; set; }

        // Relacion con Answer
        public virtual List<Answer>? Answers { get; set; }

        // Relacion con liked
        public virtual List<Like>? Likes { get; set; }
        
        // Relacion con Suscriber
        public virtual List<Suscriber> Subs { get; set; }
    }
}
