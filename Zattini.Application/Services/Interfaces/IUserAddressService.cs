using Zattini.Application.DTOs;

namespace Zattini.Application.Services.Interfaces
{
    public interface IUserAddressService
    {
        public Task<ResultService<UserAddressDTO>> Create(UserAddressDTO userAddressDTO);
    }
}
