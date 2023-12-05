

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.ScholarshipDtos;

namespace BusinessLogicLayer.Services;

public class ScholarshipService : IScholarshipService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ScholarshipService(IUnitOfWork unitOfWork,
                               IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddScholarshipAsync(AddScholarshipDto scholarshipDto)
    {
        if(scholarshipDto is null)
        {
            throw new ArgumentNullException(nameof(scholarshipDto), "Scholarship is null");
        }
        if(scholarshipDto.Amount == 0)
        {
            throw new ArgumentException("Scholarship is required");
        }
        var list = await _unitOfWork.ScholarshipRepository.GetAllAsync();
        if(list.Any(s => s.Id == scholarshipDto.StudentId))
        {
            throw new ArgumentException("Scholarship is already exist");
        }
        else 
        {
            var scholarship = _mapper.Map<Scholarship>(scholarshipDto);
            await _unitOfWork.ScholarshipRepository.AddAsync(scholarship);
            await _unitOfWork.SaveAsync();
        }
    }
    public async Task DeleteScholarshipAsync(int Id)
    {
        _unitOfWork.ScholarshipRepository.Delete(Id);
        await _unitOfWork.SaveAsync();  
    }
    public async Task<List<ScholarshipDto>> GetAllScholarshipAsync()
    {
        var list = await _unitOfWork.ScholarshipRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(s => _mapper.Map<ScholarshipDto>(s)).ToList();
    }

    public async Task<ScholarshipDto> GetScholarshipByIdAsync(int Id)
    {
        var scholarship = await _unitOfWork.ScholarshipRepository.GetByIdAsync(Id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<ScholarshipDto>(scholarship);
    }

    public async Task UpdateScholarshipAsync(ScholarshipDto newScholarshipDto)
    {
        var scholarship = _mapper.Map<Scholarship>(newScholarshipDto);
        _unitOfWork.ScholarshipRepository.Update(scholarship);
        await _unitOfWork.SaveAsync();
    }
}
