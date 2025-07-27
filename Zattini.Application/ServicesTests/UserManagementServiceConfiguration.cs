using AutoMapper;
using Moq;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Repositories;
using Zattini.Infra.Data.UtilityExternal.Interface;

namespace Zattini.Application.ServicesTests
{
    public class UserManagementServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IUserAddressRepository> UserAddressRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IUserCreateDTOValidator> UserCreateDTOValidatorMock { get; }
        public Mock<IUserAddressService> UserAddressServiceMock { get; }
        public Mock<IUserCreateAccountFunction> UserCreateAccountFunctionMock { get; }
        public Mock<IUserAddressCreateDTOValidator> UserAddressCreateDTOValidatorMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }

        public UserManagementServiceConfiguration()
        {
            UserRepositoryMock = new();
            UserAddressRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            UserCreateDTOValidatorMock = new();
            UserAddressServiceMock = new();
            UserCreateAccountFunctionMock = new();
            UserAddressCreateDTOValidatorMock = new();
            CloudinaryUtiMock = new();
        }
    }
}
