namespace ForumBackEnd.DTO
{
    public class QuestionDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
    }
}
