using AutoMapper;
using Moq;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Authentication;
using Zattini.Domain.Repositories;

namespace Zattini.Application.ServicesTests
{
    internal class UserAuthenticationServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ITokenGeneratorUser> TokenGeneratorUserMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }

        public UserAuthenticationServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            TokenGeneratorUserMock = new();
            UserCreateAccountFunctionMock = new();
        }
    }
}
