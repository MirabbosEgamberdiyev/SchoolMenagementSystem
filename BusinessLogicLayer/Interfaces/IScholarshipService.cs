

using SchoolApi.Dto.ScholarshipDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IScholarshipService
{
    Task<List<ScholarshipDto>> GetAllScholarshipAsync();
    Task<ScholarshipDto> GetScholarshipByIdAsync(int Id);
    Task AddScholarshipAsync(AddScholarshipDto scholarshipDto);
    Task UpdateScholarshipAsync(ScholarshipDto newScholarshipDto);
    Task DeleteScholarshipAsync(int Id);
    
}
