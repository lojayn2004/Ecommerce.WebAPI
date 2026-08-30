using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ApiBaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {

            return ToActionResult(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
    
            return ToActionResult(await _authService.Register(request));
        }

    }
}
