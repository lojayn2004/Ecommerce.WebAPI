using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Application.Utils;
using Ecommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Ecommerce.Application.Services
{
    public class AuthService(UserManager<ApplicationUser> _userManager,
        IOptions<JwtOptions> _jwtOptions) : IAuthService
    {
        public Task<Result<bool>> CheckEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ApplicationUser>> GetCurrentUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AddressDto>> GetUserAddressAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AuthResultDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Errors.InValidUserCredentials;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) return Errors.InValidUserCredentials;

            return Result<AuthResultDto>.Success(new AuthResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await JWTUtils.GenerateTokenAsync(user, _jwtOptions, _userManager)
            });
        }

        public async Task<Result<AuthResultDto>> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                DisplayName = registerDto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return Errors.UserCreationFailed;
            return Result<AuthResultDto>.Success(new AuthResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await JWTUtils.GenerateTokenAsync(user, _jwtOptions, _userManager)
            });

        }

        public Task<Result<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto)
        {
            throw new NotImplementedException();
        }
    }
}
