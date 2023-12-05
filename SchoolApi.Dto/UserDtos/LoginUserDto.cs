
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Dto.UserDtos;

public class LoginUserDto
{
    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}
