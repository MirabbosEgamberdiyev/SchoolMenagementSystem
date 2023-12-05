
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.SubjectDtos;

namespace BusinessLogicLayer.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddSubjectAsync(AddSubjectDto newSubjectDto)
    {
        if(newSubjectDto == null)
        {
            throw new ArgumentNullException(nameof(newSubjectDto), "New subject is null here");
        }
        if (string.IsNullOrEmpty(newSubjectDto.SubjectName))
        {
            throw new ArgumentException("Subject name is required ");
        }
        var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
        if(subjects.Any(s => s.SubjectName == newSubjectDto.SubjectName))
        {
            throw new ArgumentException("Subject is already exist");
        }
        else
        {
            var subject = _mapper.Map<Subject>(newSubjectDto);
            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteSubjectAsync(int id)
    {
        _unitOfWork.SubjectRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<SubjectDto>> GetAllSubjectAsync()
    {
        var list = await _unitOfWork.SubjectRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(s => _mapper.Map<SubjectDto>(s)).ToList();

    }

    public async Task<SubjectDto> GetSubjectByIdAsync(int id)
    {
        var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<SubjectDto>(subject);
    }

    public async Task UpdateSubjectAsync(SubjectDto subjectDto)
    {
        var subject = _mapper.Map<Subject>(subjectDto);
        _unitOfWork.SubjectRepository.Update(subject);
        await _unitOfWork.SaveAsync();
    }
}
