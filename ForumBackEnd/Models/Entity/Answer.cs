using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Reponse { get; set; }
        public string Code { get; set; }
        // Relacion con User
        public int UserId { get; set; }
        public virtual User User { get; set; }
        // Relacion con la pregunta
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        // Relacion con likes
        public virtual List<LikesAnswer> Likes { get; set; }

    }
}