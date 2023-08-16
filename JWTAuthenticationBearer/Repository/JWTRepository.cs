using JWTAuthenticationBearer.Interfaces;
using JWTAuthenticationBearer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JWTAuthenticationBearer.Repository
{
    public class JWTRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtHandlerWrapper _jwtHandler;
        private readonly string _jwtSecret;

        public JWTRepository(IConfiguration configuration, IJwtHandlerWrapper jwtHandler)
        {
            _configuration = configuration;
            _jwtHandler = jwtHandler;
        }

        public string GenerateToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                claims,
                    expires: DateTime.UtcNow.AddSeconds(30),
                    signingCredentials: credentials);

                var tokenString = _jwtHandler.WriteToken(token);

                // Configure token validation parameters
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true // Enable validation of the token's lifetime
                };

                return tokenString;
            }
            catch(Exception ex)
            {
                var error = ex.ToString();
                return null;
            }
        }
    }
}
