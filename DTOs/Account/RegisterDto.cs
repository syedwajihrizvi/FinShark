using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be atleast 3 characters")]
        [MaxLength(55, ErrorMessage = "Username exceeds 3 character length")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address must be valid")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}