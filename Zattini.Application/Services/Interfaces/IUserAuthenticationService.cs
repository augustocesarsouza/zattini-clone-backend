using Zattini.Application.DTOs;

namespace Zattini.Application.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResultService<UserDTO>> GetByIdInfoUser(string userId);
        public Task<ResultService<UserLoginDTO>> Login(string email, string password);
    }
}
