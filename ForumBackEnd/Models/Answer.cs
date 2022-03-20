namespace ForumBackEnd.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsSetted { get; set; } = false;

        // Relación con user
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Relación con Question
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        // Relacion con Liked
        //public int RecoginitionId { get; set; }
        public virtual List<Like>? Like { get; set; }
    }
}
