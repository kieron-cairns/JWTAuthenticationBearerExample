using JWTAuthenticationBearer.Interfaces;
using JWTAuthenticationBearer.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthenticationBearer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IJWTRepository _jwtRepository;
        private readonly IConfiguration _configuration;

        public JwtController(IConfiguration configuration, IJWTRepository jWTRepository)
        {
            _configuration = configuration;
            _jwtRepository = jWTRepository;
        }

        [HttpPost("/AuthenticateUser")]
        public IActionResult AuthenticateUser()
        {
            string httpHeaderUsername = Request.Headers["username"].FirstOrDefault();
            string httpHeaderPassword = Request.Headers["password"].FirstOrDefault();

            User user = new User
            {
                Username = httpHeaderUsername,
                Password = httpHeaderPassword,
                Role = "User"
            };
            try
            {
                string username = _configuration["TestUsers:Username"];
                string password = _configuration["TestUsers:Password"];


                if (httpHeaderUsername == username && httpHeaderPassword == password)
                {
                    //TODO: create JWT token upon succesfull authentication
                    var token = _jwtRepository.GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return StatusCode(401, "Incorrect credentials");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
