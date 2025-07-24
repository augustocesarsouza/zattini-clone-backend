using Microsoft.AspNetCore.Mvc;
using Zattini.Api.ControllersInterface;
using Zattini.Application.DTOs;
using Zattini.Application.Services;
using Zattini.Application.Services.Interfaces;
using Zattini.Domain.Authentication;

namespace Zattini.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IBaseController _baseController;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserManagementService userManagementService,
            IUserAuthenticationService userAuthenticationService,
            IBaseController baseController, ICurrentUser currentUser)
        {
            _userManagementService = userManagementService;
            _userAuthenticationService = userAuthenticationService;
            _baseController = baseController;
            _currentUser = currentUser;
        }

        [HttpGet("v1/user/get-by-id-info-user/{userId}")]
        public async Task<IActionResult> GetByIdInfoUser([FromRoute] string userId)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            //var result = await _userAuthenticationService.GetByIdInfoUser(userId);

            //if (result.IsSucess)
            //    return Ok(result);

            //return BadRequest(result);

            return Ok(new UserDTO());
        }

        [HttpGet("v1/public/user/login/{email}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
        {
            //var userAuth = _baseController.Validator(_currentUser);
            //if (userAuth == null)
            //    return _baseController.Forbidden();

            var result = await _userAuthenticationService.Login(email, password);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/public/user/create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            var result = await _userManagementService.Create(userCreateDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
