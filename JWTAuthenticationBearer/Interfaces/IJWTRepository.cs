using JWTAuthenticationBearer.Models;

namespace JWTAuthenticationBearer.Interfaces
{
    public interface IJWTRepository
    {
        string GenerateToken(User user);
    }
}