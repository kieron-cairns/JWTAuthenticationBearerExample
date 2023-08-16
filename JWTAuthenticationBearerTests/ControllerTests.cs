using Microsoft.Extensions.Configuration;
using JWTAuthenticationBearer.Interfaces;
using JWTAuthenticationBearer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuthenticationBearer.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthenticationBearerTests
{
    public class ControllerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IJWTRepository> _mockJwtRepository;
        private readonly User _mockUser;

        public ControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockJwtRepository = new Mock<IJWTRepository>();
            _mockUser = new User { Username = "TestUsername", Password = "TestPassword", Role = "testRole" };

            _mockConfiguration.SetupGet(x => x["TestUsers:Username"]).Returns("TestUsername");
            _mockConfiguration.SetupGet(x => x["TestUsers:Password"]).Returns("TestPassword");
        }

        [Fact] 
        public async Task AuthenticateUser_Returns_OK()
        {
            //Arrange
            var jwtReppsitoryMock = new Mock<IJWTRepository>();
            jwtReppsitoryMock.Setup(r => r.GenerateToken(_mockUser)).Returns("testToken");

            var controller = new JwtController(_mockConfiguration.Object, _mockJwtRepository.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Request.Headers["username"] = "TestUsername";
            controller.HttpContext.Request.Headers["password"] = "TestPassword";
            
            //Act
            var result = controller.AuthenticateUser();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
