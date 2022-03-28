namespace ForumBackEnd.Services.PasswordRepository
{
    public class PasswordRepository: IPasswordRepository
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string dbPassword, string entryPassword)
        {
            return BCrypt.Net.BCrypt.Verify(entryPassword, dbPassword);
        }
    }
}
