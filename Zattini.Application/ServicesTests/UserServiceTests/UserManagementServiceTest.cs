using FluentValidation.Results;
using Moq;
using Xunit;
using Zattini.Application.DTOs;
using Zattini.Application.Services;
using Zattini.Domain.Entities;

namespace Zattini.Application.ServicesTests.UserServiceTests
{
    public class UserManagementServiceTest
    {
        private readonly UserManagementServiceConfiguration _userManagementServiceConfiguration;
        private readonly UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            _userManagementServiceConfiguration = new();
            var userManagementService = new UserManagementService(_userManagementServiceConfiguration.UserRepositoryMock.Object,
                _userManagementServiceConfiguration.UserAddressRepositoryMock.Object, _userManagementServiceConfiguration.MapperMock.Object,
                _userManagementServiceConfiguration.UnitOfWorkMock.Object, _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Object,
                _userManagementServiceConfiguration.UserAddressServiceMock.Object, _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Object,
                _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Object, _userManagementServiceConfiguration.CloudinaryUtiMock.Object);

            _userManagementService = userManagementService;
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            //UserCreateDTO(string ? name, string ? lastName, string ? gender, string ? email,
            //string ? birthDate, string ? cpf, string ? cellPhone, string ? password, string ? userImageBase64, UserAddressDTO ? userAddressDTO)

            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("augusto cesar", "souza santana", "male", "augustocesarsantana53@gmail.com", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "23232-455", "rua", "rua cajazeiras", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserAddressDTO>()))
                .Returns(new ValidationResult());

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
                .Returns(byteUser);

            var hashedPassword = "asdasddasasdasdasd";

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(hashedPassword);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserAddressRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<UserAddress>()))
                .ReturnsAsync(new UserAddress());

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Validate_UserCreateDTO_CreateAsync_Success()
        {
            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("", "", "male", "", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "23232-455", "rua", "rua cajazeiras", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("PropertyName", "Error message 1")
                }));

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("validation User error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Validate_UserAddressDTO_CreateAsync_Success()
        {
            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("augusto cesar", "souza santana", "male", "augustocesarsantana53@gmail.com", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "", "", "", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserAddressDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("PropertyName", "Error message 1")
                }));

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("validation UserAddress error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Return_Null_When_Create_Account_RepositoryUser_CreateAsync()
        {
            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("augusto cesar", "souza santana", "male", "augustocesarsantana53@gmail.com", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "23232-455", "rua", "rua cajazeiras", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserAddressDTO>()))
                .Returns(new ValidationResult());

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
                .Returns(byteUser);

            var hashedPassword = "asdasddasasdasdasd";

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(hashedPassword);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create user null value", result.Message);
        }

        [Fact]
        public async Task Should_Return_Null_When_Create_Account_RepositoryUserAddress_CreateAsync()
        {
            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("augusto cesar", "souza santana", "male", "augustocesarsantana53@gmail.com", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "23232-455", "rua", "rua cajazeiras", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserAddressDTO>()))
                .Returns(new ValidationResult());

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
                .Returns(byteUser);

            var hashedPassword = "asdasddasasdasdasd";

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(hashedPassword);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User());

            _userManagementServiceConfiguration.UserAddressRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<UserAddress>()))
                .ReturnsAsync((UserAddress?)null);

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create UserAddress null value", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Create_Account_RepositoryUser()
        {
            var idUserAddress = Guid.NewGuid();

            var userCreateDTO = new UserCreateDTO("augusto cesar", "souza santana", "male", "augustocesarsantana53@gmail.com", "05/10/1999",
                "232.332.233-22", "(43) 65565-6565", "Augusto92349923", "",
                new UserAddressDTO(null, "23232-455", "rua", "rua cajazeiras", 112,
                "", "asdasddas", "Mato grosso do sul", "Campo Grande", "", idUserAddress, null));

            _userManagementServiceConfiguration.UserCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserCreateDTO>()))
                .Returns(new ValidationResult());

            _userManagementServiceConfiguration.UserAddressCreateDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<UserAddressDTO>()))
                .Returns(new ValidationResult());

            var byteUser = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.GenerateSalt())
                .Returns(byteUser);

            var hashedPassword = "asdasddasasdasdasd";

            _userManagementServiceConfiguration.UserCreateAccountFunctionMock.Setup(valid => valid.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(hashedPassword);

            _userManagementServiceConfiguration.UserRepositoryMock.Setup(valid => valid.CreateAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("error when create user in repository"));

            var result = await _userManagementService.Create(userCreateDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("error when create user in repository", result.Message);
        }
    }
}
