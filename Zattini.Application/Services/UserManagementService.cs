using AutoMapper;
using Zattini.Application.DTOs;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Entities;
using Zattini.Domain.Repositories;
using Zattini.Infra.Data.CloudinaryConfigClass;
using Zattini.Infra.Data.Repositories;
using Zattini.Infra.Data.UtilityExternal.Interface;

namespace Zattini.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;
        private readonly IUserAddressService _userAddressService;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly IUserAddressCreateDTOValidator _userAddressCreateDTOValidator;
        private readonly ICloudinaryUti _cloudinaryUti;

        public UserManagementService(IUserRepository userRepository, IUserAddressRepository userAddressRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IUserCreateDTOValidator userCreateDTOValidator, IUserAddressService userAddressService, IUserCreateAccountFunction userCreateAccountFunction,
            IUserAddressCreateDTOValidator userAddressCreateDTOValidator, ICloudinaryUti cloudinaryUti)
        {
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userCreateDTOValidator = userCreateDTOValidator;
            _userAddressService = userAddressService;
            _userCreateAccountFunction = userCreateAccountFunction;
            _userAddressCreateDTOValidator = userAddressCreateDTOValidator;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<CreateUserDTO>> Create(UserCreateDTO? userCreateDTO)
        {
            if (userCreateDTO == null)
                return ResultService.Fail<CreateUserDTO>("userDTO is null");

            var userAddressDTO = userCreateDTO.UserAddressDTO;

            if (userAddressDTO == null)
                return ResultService.Fail<CreateUserDTO>("userAddressDTO is null");

            var validationUser = _userCreateDTOValidator.ValidateDTO(userCreateDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<CreateUserDTO>("validation User error check the information", validationUser);

            var validationUserAddressDTO = _userAddressCreateDTOValidator.ValidateDTO(userAddressDTO);

            if (!validationUserAddressDTO.IsValid)
                return ResultService.RequestError<CreateUserDTO>("validation UserAddress error check the information", validationUserAddressDTO);

            try
            {
                await _unitOfWork.BeginTransaction();

                string password = userCreateDTO.Password ?? "";

                byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();
                // Hash the password with the salt
                string hashedPassword = _userCreateAccountFunction.HashPassword(password, saltBytes);
                string base64Salt = Convert.ToBase64String(saltBytes);

                byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

                Guid idUser = Guid.NewGuid();

                User userCreate = new User();
                string birthDateString = userCreateDTO.BirthDate ?? "";

                var birthDateSlice = birthDateString.Split("/");
                var day = birthDateSlice[0];
                var month = birthDateSlice[1];
                var year = birthDateSlice[2];

                var birthDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                var createUserDTO = new CreateUserDTO(false, null);

                if (userCreateDTO.UserImageBase64?.Length > 0)
                {
                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(userCreateDTO.UserImageBase64, "imgs-backend-frontend-zattini/img-user", 320, 320);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<CreateUserDTO>("error when create ImgPerfil");
                    }

                    var img = result.ImgUrl;

                    userCreate = new User(idUser, userCreateDTO.Name, userCreateDTO.LastName, userCreateDTO.Gender,
                        userCreateDTO.Email, birthDateUtc, userCreateDTO.Cpf, userCreateDTO.CellPhone, hashedPassword, base64Salt, img, new List<UserAddress>());
                }
                else
                {
                    userCreate = new User(idUser, userCreateDTO.Name, userCreateDTO.LastName, userCreateDTO.Gender,
                        userCreateDTO.Email, birthDateUtc, userCreateDTO.Cpf, userCreateDTO.CellPhone, hashedPassword, base64Salt, null, new List<UserAddress>());
                }

                var data = await _userRepository.CreateAsync(userCreate);

                if (data == null)
                    return ResultService.Fail<CreateUserDTO>("error when create user null value");

                userAddressDTO.SetUserId(idUser);
                // nao sei oque fazer amanha seila continuar o frontend ou ver como está a criação e tal, depois login

                Guid idUserAddress = Guid.NewGuid();
                var userAddress = new UserAddress(idUserAddress, userAddressDTO.Cep, userAddressDTO.TypeAddress,
                    userAddressDTO.Address, userAddressDTO.Number, userAddressDTO.Complement, userAddressDTO.Neighborhood,
                    userAddressDTO.State, userAddressDTO.City, userAddressDTO.ReferencePoint, userAddressDTO.UserId, null);

                var dataUserAddress = await _userAddressRepository.CreateAsync(userAddress);

                if (dataUserAddress == null)
                    return ResultService.Fail<CreateUserDTO>("error when create UserAddress null value");

                await _unitOfWork.Commit();

                data.SetPasswordHash("");
                data.SetSalt("");

                var userDTOMap = _mapper.Map<UserDTO>(data);
                createUserDTO.SetTokenIsValid(true);
                createUserDTO.SetUserDTO(userDTOMap);

                return ResultService.Ok(createUserDTO);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CreateUserDTO>(ex.Message);
            }
        }
    }
}
