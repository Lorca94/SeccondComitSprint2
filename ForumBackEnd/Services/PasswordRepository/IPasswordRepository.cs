namespace ForumBackEnd.Services.PasswordRepository
{
    public interface IPasswordRepository
    {
        string HashPassword(string password);
        bool VerifyPassword(string dbPassword, string entryPassword);
    }
}
