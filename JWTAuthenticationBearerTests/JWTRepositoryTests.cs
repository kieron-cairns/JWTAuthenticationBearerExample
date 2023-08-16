using JWTAuthenticationBearer.Interfaces;
using JWTAuthenticationBearer.Models;
using JWTAuthenticationBearer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace JWTAuthenticationBearerTests
{
    public class JWTRepositoryTests
    {
        private readonly Mock<IJwtHandlerWrapper> _jwtHandlerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly User _user;

        public JWTRepositoryTests()
        {
            _jwtHandlerMock = new Mock<IJwtHandlerWrapper>();
            _configurationMock = new Mock<IConfiguration>();
            _user = new User { Username = "TestUsername", Password = "TestPassword", Role = "testRole" };

            // Setup mock configuration values
            _configurationMock.SetupGet(x => x["Jwt:Secret"]).Returns("your-secret-value");
            _configurationMock.SetupGet(x => x["Jwt:Issuer"]).Returns("your-issuer-value");
            _configurationMock.SetupGet(x => x["Jwt:Audience"]).Returns("your-issuer-value");
        }

        [Fact]
        public void Generate_JWT_Token_Succesfully_Writes_Token()
        {
            //Arrange
            _jwtHandlerMock.Setup(th => th.WriteToken(It.IsAny<SecurityToken>())).Returns("fakeTokenString");
           
            var repository = new JWTRepository(_configurationMock.Object, _jwtHandlerMock.Object);

            //Act
            var token = repository.GenerateToken(_user);

            //Assert
            Assert.Equal("fakeTokenString", token);
        }
    }
}