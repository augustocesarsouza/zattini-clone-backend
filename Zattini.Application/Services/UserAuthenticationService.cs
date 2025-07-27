using AutoMapper;
using System.Text;
using Zattini.Application.DTOs;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Authentication;
using Zattini.Domain.Repositories;

namespace Zattini.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorUser _tokenGeneratorUser;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;

        public UserAuthenticationService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, 
            ITokenGeneratorUser tokenGeneratorUser, IUserCreateAccountFunction userCreateAccountFunction)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenGeneratorUser = tokenGeneratorUser;
            _userCreateAccountFunction = userCreateAccountFunction;
        }

        public Task<ResultService<UserDTO>> GetByIdInfoUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultService<UserLoginDTO>> Login(string email, string password)
        {
            try 
            {
                var user = await _userRepository.GetUserInfoToLogin(email);

                if (user == null)
                    return ResultService.Fail(new UserLoginDTO(false, new UserDTO()));

                var passwordHash = user.GetPasswordHash();
                var salt = user.GetSalt();

                if (passwordHash == null || salt == null)
                    return ResultService.Fail<UserLoginDTO>("Error password hash or salt is null");

                string storedHashedPassword = passwordHash;

                string enteredPassword = password;

                // Convert the stored salt and entered password to byte arrays
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                byte[] storedSaltBytes = Convert.FromBase64String(salt);
                // Concatenate entered password and stored salt
                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];

                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

                var userReturnToFrontend = new UserDTO();

                // Hash the concatenated value
                string enteredPasswordHash = _userCreateAccountFunction.HashPassword(enteredPassword, storedSaltBytes);

                // Compare the entered password hash with the stored hash
                if (enteredPasswordHash == storedHashedPassword)
                {
                    userReturnToFrontend.SetName(user.Name);
                    userReturnToFrontend.SetEmail(user.Email);
                    userReturnToFrontend.SetId(user.GetId());

                    var token = _tokenGeneratorUser.Generator(user);

                    if (!token.IsSucess)
                        return ResultService.Fail<UserLoginDTO>(token.Message ?? "error validate token");

                    var tokenValue = token.Data.Acess_Token;

                    if (tokenValue != null)
                    {
                        userReturnToFrontend.SetToken(tokenValue);
                    }

                    return ResultService.Ok(new UserLoginDTO(true, userReturnToFrontend));
                }

                return ResultService.Ok(new UserLoginDTO(false, userReturnToFrontend));
            }
            catch(Exception ex)
            {
                return ResultService.Fail<UserLoginDTO>(ex.Message);
            }
        }
    }
}
