using Microsoft.AspNetCore.Mvc;
using Zattini.Application.DTOs;
using Zattini.Domain.Authentication;

namespace Zattini.Api.ControllersInterface
{
    public interface IBaseController
    {
        public UserAuthDTO? Validator(ICurrentUser? currentUser);
        public IActionResult Forbidden();
    }
}
