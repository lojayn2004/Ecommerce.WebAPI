

using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Domain.Models.Identity;

namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IAuthService
    {

        Task<Result<AuthResultDto>> Login(LoginDto loginDto);

        Task<Result<AuthResultDto>> Register(RegisterDto registerDto);


        Task<Result<bool>> CheckEmailAsync(string email);

        // need to change to user dto
        Task<Result<ApplicationUser>> GetCurrentUserAsync(string email);

        Task<Result<AddressDto>> GetUserAddressAsync(string email);

        Task<Result<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto);



    }
}
