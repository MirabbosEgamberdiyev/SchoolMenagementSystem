
using SchoolApi.Dto.ParentDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IParentService
{
    Task<List<ParentDto>> GetAllParentsAsync();
    Task<ParentDto> GetParentByIdAsync(int id);
    Task AddParentAsync(AddParentDto parentDto);
    Task UpdateParentAsync(ParentDto parentDto);
    Task DeleteParentAsync(int id);

}
