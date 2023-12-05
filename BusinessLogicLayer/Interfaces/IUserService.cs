

using BusinessLogicLayer.Helpers;
using SchoolApi.Dto.UserDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    Task<AuthResult> RegisterUserAsync(RegisterUserDto dto, string role);
    Task<LoginResult> LoginUserAsync(LoginUserDto dto);

    Task<AuthResult> ChangePasswordAsync(ChangePasswordDto dto);
    Task<AuthResult> DeleteAccountAsync(string email);
}
