using Ecommerce.Application.Dtos.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.api.Controllers
{
    
    public class ApiBaseController: ControllerBase
    {
        protected string UserEmail => User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("User Is Not Authorized");


        protected IActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess == true)
                return Ok(new { data = result.Data, success = true });

            if (result.ErrorType == ErrorType.Unauthorized)
                return Unauthorized(new { message = result.Message, sucess = false });
            if (result.ErrorType == ErrorType.Validation)
                return BadRequest(new { message = result.Message, success = false });
            if (result.ErrorType == ErrorType.NotFound)
                return NotFound(new { message = result.Message, success = false });

            return Problem(detail: result.Message);

        }


    }
}
