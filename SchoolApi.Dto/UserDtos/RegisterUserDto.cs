using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Dto.UserDtos;

public class RegisterUserDto : LoginUserDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
}