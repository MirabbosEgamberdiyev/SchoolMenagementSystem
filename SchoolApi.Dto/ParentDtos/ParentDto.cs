
namespace SchoolApi.Dto.ParentDtos;

public class ParentDto:BaseEntityDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}
