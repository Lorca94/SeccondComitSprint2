namespace ForumBackEnd.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsSetted { get; set; } = false;

        // Relacion con User
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        // Relacion con Module
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        // Relacion con Answer
        public virtual List<Answer>? Answers { get; set; }

        // Relacion con Liked
        public virtual List<Like>? Like { get; set; }
        public virtual List<Suscriber> Subs { get; set; }



    }
}
