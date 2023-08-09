using Microsoft.IdentityModel.Tokens;

namespace JWTAuthenticationBearer.Interfaces
{
    public interface IJwtHandlerWrapper
    {
        string WriteToken(SecurityToken token);
    }
}
