using AutoMapper;
using Zattini.Application.DTOs;
using Zattini.Application.DTOs.Validations.Interfaces;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Entities;
using Zattini.Domain.Repositories;

namespace Zattini.Application.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAddressCreateDTOValidator _userAddressCreateDTOValidator;

        public UserAddressService(IUserAddressRepository userAddressRepository, IMapper mapper, 
            IUnitOfWork unitOfWork, IUserAddressCreateDTOValidator userAddressCreateDTOValidator)
        {
            _userAddressRepository = userAddressRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userAddressCreateDTOValidator = userAddressCreateDTOValidator;
        }

        public async Task<ResultService<UserAddressDTO>> Create(UserAddressDTO userAddressDTO)
        {
            //var validationUserAddressDTO = _userAddressCreateDTOValidator.ValidateDTO(userAddressDTO);

            //if (!validationUserAddressDTO.IsValid)
            //    return ResultService.RequestError<UserAddressDTO>("validation UserAddress error check the information", validationUserAddressDTO);

            try
            {
                await _unitOfWork.BeginTransaction();

                Guid idUserAddress = Guid.NewGuid();
                var userAddress = new UserAddress(idUserAddress, userAddressDTO.Cep, userAddressDTO.TypeAddress,
                    userAddressDTO.Address, userAddressDTO.Number, userAddressDTO.Complement, userAddressDTO.Neighborhood,
                    userAddressDTO.State, userAddressDTO.City, userAddressDTO.ReferencePoint, userAddressDTO.UserId, null);

                var data = await _userAddressRepository.CreateAsync(userAddress);

                if (data == null)
                    return ResultService.Fail<UserAddressDTO>("error when create user null value");

                await _unitOfWork.Commit();

                var userAddressDTOMap = _mapper.Map<UserAddressDTO>(data);

                return ResultService.Ok(userAddressDTOMap);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserAddressDTO>(ex.Message);
            }
        }
    }
}
