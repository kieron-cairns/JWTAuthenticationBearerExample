using JWTAuthenticationBearer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace JWTAuthenticationBearerTests
{
    public class JWTWrapperTests
    {
        private readonly Mock<IJwtHandlerWrapper> _jwtTokenHandlerMock;

        [Fact]
        public void Generate_JWT_Token_Succesfully_Writes_Token()
        {
            _jwtTokenHandlerMock.Setup(th => th.WriteToken(It.IsAny<SecurityToken>())).Returns("fakeTokenString");

        }
    }
}