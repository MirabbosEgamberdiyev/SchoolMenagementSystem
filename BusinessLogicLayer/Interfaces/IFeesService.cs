

using SchoolApi.Dto.FeesDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IFeesService
{
    Task<List<FeesDto>> GetAllFeesAsync();
    Task<FeesDto> GetFeesByIdAsync(int id);
    Task AddFeesAsync(AddFeesDto newFees);
    Task UpdateFeesAsync(FeesDto feesDto);
    Task DeleteFeesAsync(int id);
}
