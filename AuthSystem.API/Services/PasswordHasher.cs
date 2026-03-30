namespace AuthSystem.API.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Hash the password using BCrypt
            // The HashPassword method automatically generates
            // a salt and combines it with the password before hashing
    
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }
        public bool VerifyPassword(string password, string passwordHash)
        {
            // Verify the password against the stored hash
            // The Verify method extracts the salt from the hash and uses it to hash the input password for comparison
            // It returns true if the password matches the hash, and false otherwise
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
