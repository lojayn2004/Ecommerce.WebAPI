using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Application.Dtos.ResultPattern;

namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IAuthService
    {

        Task<Result<AuthResultDto>> Login(LoginDto loginDto);

        Task<Result<AuthResultDto>> Register(RegisterDto registerDto);

    }
}
