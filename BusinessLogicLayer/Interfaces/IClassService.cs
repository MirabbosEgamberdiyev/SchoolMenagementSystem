



using SchoolApi.Dto.ClassDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IClassService
{
    Task<List<ClassDto>> GetAllClassAsync();
    Task<ClassDto> GetClassByIdAsync(int id);
    Task AddClassAsync(AddClassDto newClass);
    Task UpdateClassAsync(ClassDto classDto);
    Task DeleteClassAsync(int id);
}
