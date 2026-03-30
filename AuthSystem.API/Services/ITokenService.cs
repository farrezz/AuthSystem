using AuthSystem.API.Models;

namespace AuthSystem.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
