using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.api.Controllers
{
    
    public class ApiBaseController: ControllerBase
    {
        protected string UserEmail => User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException("User Is Not Authorized");
    }
}
