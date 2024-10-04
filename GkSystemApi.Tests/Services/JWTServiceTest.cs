using gk_system_api.Models;
using gk_system_api.Services;
using gk_system_api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace GkSystemApi.Tests.Services
{
    public class JWTServiceTest
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly IJWTService _jwtService;
        private readonly DecodedUrl _exampleCredentials;
        public JWTServiceTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _jwtService = new JWTService(_configurationMock.Object);
            _exampleCredentials = new DecodedUrl()
            {
                Login = "test_login",
                Password = "test_password"
            };

            _configurationMock.SetupGet(s => s["JwtSettings:SecurityKey"]).Returns(Guid.NewGuid().ToString());
            _configurationMock.SetupGet(s => s["JwtSettings:ExpirationTimeInMinutes"]).Returns("12");
        }

        [Fact]
        public void TestGetJWTLoginToken()
        {
            var resultToken = _jwtService.GetToken(_exampleCredentials);
            Assert.NotNull(resultToken);
            Assert.Equal(12, resultToken.ExpiresInMin);
            Assert.NotEmpty(resultToken.Token);

            resultToken = _jwtService.GetToken(_exampleCredentials, 8);
            Assert.Equal(8, resultToken.ExpiresInMin);
        }
    }
}
