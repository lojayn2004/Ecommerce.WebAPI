

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = default!;

        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string DisplayName { get; set; } = default!;
    }

}
