
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.FeesDtos;

namespace BusinessLogicLayer.Services;

public class FeesService : IFeesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FeesService(IUnitOfWork unitOfWork,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddFeesAsync(AddFeesDto newFees)
    {
        if(newFees == null)
        {
            throw new ArgumentNullException(nameof(newFees), "Fees is null here ");
        }
        if (newFees.FeesAmount == 0)
        {
            throw new ArgumentException("Fees is null");
        }
        var list = await _unitOfWork.FeesRepository.GetAllAsync();
        if(list.Any(f => f.Id==newFees.ClassId))
        {
            throw new ArgumentException("Fees is already exist here");
        }
        else
        {
            var fees =  _mapper.Map<Fees>(newFees);
            await _unitOfWork.FeesRepository.AddAsync(fees);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteFeesAsync(int id)
    {
        _unitOfWork.FeesRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<FeesDto>> GetAllFeesAsync()
    {
        var feeses = await _unitOfWork.FeesRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return feeses.Select(fees => _mapper.Map<FeesDto>(fees)).ToList();
    }

    public async Task<FeesDto> GetFeesByIdAsync(int id)
    {
        var fees = await _unitOfWork.FeesRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<FeesDto>(fees);
    }

    public async Task UpdateFeesAsync(FeesDto feesDto)
    {
        var fees = _mapper.Map<Fees>(feesDto);
        _unitOfWork.FeesRepository.Update(fees);
        await _unitOfWork.SaveAsync();
    }
}
