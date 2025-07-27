using Moq;
using Xunit;
using Zattini.Application.Services;
using Zattini.Domain.Authentication;
using Zattini.Domain.Entities;
using Zattini.Domain.InfoErrors;

namespace Zattini.Application.ServicesTests.UserServiceTests
{
    public class UserAuthenticationServiceTest
    {
        private readonly UserAuthenticationServiceConfiguration _userAuthenticationServiceConfiguration;
        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTest()
        {
            _userAuthenticationServiceConfiguration = new();
            var userAuthenticationService = new UserAuthenticationService(_userAuthenticationServiceConfiguration.UserRepositoryMock.Object,
                _userAuthenticationServiceConfiguration.MapperMock.Object, _userAuthenticationServiceConfiguration.UnitOfWorkMock.Object,
                _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Object, _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Object);
            _userAuthenticationService = userAuthenticationService;
        }

        [Fact]
        public async Task Should_Login_Successfully()
        {
            var email = "augusto@gmail.com";
            var password = "casa123";
            var passwordHash = "asdasdasdasd";

            var user = new User(Guid.NewGuid(), "augusto", null, null, "augusto@gmail.com", null, null, null,
              passwordHash, "daasdasdasdasdadsasdasdasdasdasd", null, new List<UserAddress>());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passwordHash);

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddHours(5);
            tokenOutValue.ValidateToken("token1234", expires);

            var token = InfoErrors.Ok(tokenOutValue);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Setup(valid => valid.Generator(It.IsAny<User>()))
                .Returns(token);

            var result = await _userAuthenticationService.Login(email, password);
            var entityDTO = result.Data;
            Assert.True(result.IsSucess);
            Assert.True(entityDTO?.PasswordIsCorrect);
        }

        [Fact]
        public async Task Should_Return_Error_Password_Is_Wrong_Login()
        {
            var email = "augusto@gmail.com";
            var password = "casa123";

            var user = new User(Guid.NewGuid(), "augusto", null, null, "augusto@gmail.com", null, null, null,
              "asdasdasdasdadsasdasdasd", "daasdasdasdasdadsasdasdasdasdasd", null, new List<UserAddress>());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserInfoToLogin(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userAuthenticationServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns("as2323dasdasdasdadsasdasdasd");

            var tokenOutValue = new TokenOutValue();
            var expires = DateTime.UtcNow.AddHours(5);
            tokenOutValue.ValidateToken("token1234", expires);

            var token = InfoErrors.Ok(tokenOutValue);

            _userAuthenticationServiceConfiguration.TokenGeneratorUserMock.Setup(valid => valid.Generator(It.IsAny<User>()))
                .Returns(token);

            var result = await _userAuthenticationService.Login(email, password);
            var entityDTO = result.Data;
            Assert.True(result.IsSucess);
            Assert.False(entityDTO?.PasswordIsCorrect);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Get_User_Repository_Login()
        {
            var email = "augusto@gmail.com";
            var password = "casa123";
            var passwordHash = "asdasdasdasd";

            var user = new User(Guid.NewGuid(), "augusto", null, null, "augusto@gmail.com", null, null, null,
              passwordHash, "daasdasdasdasdadsasdasdasdasdasd", null, new List<UserAddress>());

            _userAuthenticationServiceConfiguration.UserRepositoryMock.Setup(valid => valid.GetUserInfoToLogin(It.IsAny<string>()))
                .ThrowsAsync(new Exception("error when create user in repository"));

            var result = await _userAuthenticationService.Login(email, password);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create user in repository", result.Message);
        }
    }
}
