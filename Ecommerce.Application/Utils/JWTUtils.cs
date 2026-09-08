using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ecommerce.Application.Utils
{
    internal static class JWTUtils
    {
        public static async Task<string> GenerateTokenAsync(ApplicationUser applicationUser, 
            IOptions<JwtOptions> _jwtOptions, 
            UserManager<ApplicationUser> _userManager)
        {
            var claims = await GetClaims(applicationUser, _userManager);
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddDays(7)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;


        }

       
        private async static Task<List<Claim>> GetClaims(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
