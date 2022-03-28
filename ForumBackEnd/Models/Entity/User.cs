using ForumBackEnd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }

        // Suscripciones a cursos
        public virtual List<CourseUser>? CourseUser { get; set; }

        // Suscripciones a modules
        public virtual List<ModuleUser>? ModuleUser { get; set; }

        // Preguntas creadas
        public virtual List<Question> Questions { get; set; }

        // Repuestas creadas
        public virtual List<Answer> Answers { get; set; }

        // Suscripciones a preguntas
        public virtual List<SubQuestion> SubQuestions { get; set; }

        // Preguntas que le han gustado
        public virtual List<LikesQuestion> LikesQuestions { get; set; }

        // Respuestas que le han gustado
        public virtual List<LikesAnswer> LikesAnswers { get; set; }

    }
}
