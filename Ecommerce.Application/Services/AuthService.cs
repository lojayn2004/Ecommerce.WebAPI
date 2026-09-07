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
       

        public async Task<Result<AuthResultDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) 
                return Result<AuthResultDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return Result<AuthResultDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

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
            {
                string message = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<AuthResultDto>.Failure(ErrorType.Validation, message);
            }
            return Result<AuthResultDto>.Success(new AuthResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await JWTUtils.GenerateTokenAsync(user, _jwtOptions, _userManager)
            });

        }
    }
}
