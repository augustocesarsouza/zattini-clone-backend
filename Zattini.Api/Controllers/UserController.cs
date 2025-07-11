using Microsoft.AspNetCore.Mvc;
using Zattini.Application.DTOs;

namespace Zattini.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController() 
        {
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

            return Ok(new UserDTO("seila"));
        }
    }
}
