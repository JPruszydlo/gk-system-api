using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace GkSystemApi.Tests.Services
{
    public class AuthenticationServiceTest
    {
        private readonly Mock<IDatabaseService> _databaseServiceMock;
        private readonly Mock<IJWTService> _jwtServiceMock;
        private readonly Mock<ILogger<AuthenticationService>> _loggerMock;
        private readonly AuthenticationService _authenticationServiceMock;
        private readonly DecodedUrl _exampleUser;

        public AuthenticationServiceTest()
        {
            _databaseServiceMock = new Mock<IDatabaseService>();
            _loggerMock = new Mock<ILogger<AuthenticationService>>();
            _jwtServiceMock = new Mock<IJWTService>();
            _authenticationServiceMock = new AuthenticationService(
                _jwtServiceMock.Object, _databaseServiceMock.Object, _loggerMock.Object
            );
            _exampleUser = new DecodedUrl()
            {
                Login = "test_user_login",
                Password = "test_user_password"
            };

            _jwtServiceMock.Setup(s => s.GetToken(It.IsAny<DecodedUrl>(), null)).Returns(new LoginToken() { Token = "test_token" });
            _jwtServiceMock.Setup(s => s.GetToken(It.IsAny<DecodedUrl>(), It.IsAny<int>())).Returns(new LoginToken() { Token = "test_token", ExpiresInMin = 10 });
        }
        
        [Fact]
        public void TestGetToken()
        {
            var actualUserAuthenticated = _authenticationServiceMock.AuthenticateUser(_exampleUser);
            Assert.NotNull(actualUserAuthenticated);
            Assert.Equal("test_token", actualUserAuthenticated.Token);

            var actualClientAuthorized = _authenticationServiceMock.AuthorizeClient(_exampleUser);
            Assert.NotNull(actualClientAuthorized);
            Assert.Equal("test_token", actualClientAuthorized.Token);
            Assert.Equal(10, actualClientAuthorized.ExpiresInMin);
        }
        
        [Fact]
        public void TestSetNewPasswordSuccess()
        {
            _databaseServiceMock.Setup(s => s.GetUserByLogin(It.IsAny<string>())).Returns(new gk_system_api.Entities.User() { ResetToken = "test_token", ResetTokenExpiredAt = DateTime.Now.AddMinutes(10).Ticks });
            _databaseServiceMock.Setup(s => s.UpdateModel(It.IsAny<User>()));
            var actualTokenSuccess = _authenticationServiceMock.SetNewPassword(_exampleUser, "test_token");
            Assert.True(actualTokenSuccess);
        }

        [Fact]
        public void TestSetNewPasswordNoAuthHeader()
        {
            var actualNoAuthHeader = _authenticationServiceMock.SetNewPassword(null, String.Empty);
            Assert.False(actualNoAuthHeader);
        }

        [Fact]
        public void TestSetNewPasswordUserNotFound()
        {
            _databaseServiceMock.Setup(s => s.GetUserByLogin(It.IsAny<string>()));
            var actualUserNotFound = _authenticationServiceMock.SetNewPassword(_exampleUser, string.Empty);
            Assert.False(actualUserNotFound);
        }

        [Fact]
        public void TestSetNewPasswordTokenInvalid()
        {
            _databaseServiceMock.Setup(s => s.GetUserByLogin(It.IsAny<string>())).Returns(new gk_system_api.Entities.User() { ResetToken = "test_token" });
            var actualTokenInvalid= _authenticationServiceMock.SetNewPassword(_exampleUser, "test_token_2");
            Assert.False(actualTokenInvalid);
        }

        [Fact]
        public void TestSetNewPasswordTokenExpired()
        {
            _databaseServiceMock.Setup(s => s.GetUserByLogin(It.IsAny<string>())).Returns(new gk_system_api.Entities.User() { ResetToken = "test_token", ResetTokenExpiredAt = new DateTime().Ticks });
            var actualTokenExpired = _authenticationServiceMock.SetNewPassword(_exampleUser, "test_token");
            Assert.False(actualTokenExpired);
        }

        [Fact]
        public void TestSetNewPasswordException()
        {
            _databaseServiceMock.Setup(s => s.GetUserByLogin(It.IsAny<string>())).Throws(new Exception());
            var actualTokenExpired = _authenticationServiceMock.SetNewPassword(_exampleUser, String.Empty);
            Assert.False(actualTokenExpired);
        }
    }
}
