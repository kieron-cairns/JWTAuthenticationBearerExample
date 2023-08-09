using JWTAuthenticationBearer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthenticationBearer.Wrappers
{

    public class JWTHandlerWrapper : IJwtHandlerWrapper
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JWTHandlerWrapper()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string WriteToken(SecurityToken token)
        {
            return _tokenHandler.WriteToken(token);
        }
    }
}
