namespace ForumBackEnd.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleType { get; set; }

        // Relacion con User
        public virtual List<User>? Users { get; set; }
    }
}
