using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsSetted { get; set; } = false;

        // Relación con el creador de question
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Relacion con un module
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }


        // Usuarios suscritos
        public virtual List<SubQuestion>? Subs { get; set; }

        // Respuestas escritas
        public virtual List<Answer>? Answers { get; set; }

        // Likes de la pregunta
        public virtual List<LikesQuestion>? Likes { get; set; }
    }
}
