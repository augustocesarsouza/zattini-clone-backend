using Zattini.Application.DTOs;

namespace Zattini.Application.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<ResultService<CreateUserDTO>> Create(UserCreateDTO? userCreateDTO);
    }
}
