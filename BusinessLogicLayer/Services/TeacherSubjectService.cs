
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.TeacherSubjectDtos;

namespace BusinessLogicLayer.Services;

public class TeacherSubjectService : ITeacherSubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeacherSubjectService(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddTeacherSubjectAsync(AddTeacherSubjectDto newTeacherSubject)
    {
        if (newTeacherSubject == null)
        {
            throw new ArgumentNullException(nameof(newTeacherSubject), "Teacher subject is null here");
        }

        if (newTeacherSubject.SubjectId == 0) 
        {
            throw new ArgumentNullException("Subject is null here");
        }

        var list = await _unitOfWork.TeacherSubjectRepository.GetAllAsync();

        if (list.Any(ts => ts.SubjectId == newTeacherSubject.SubjectId))
        {
            throw new ArgumentException("Subject is already exist");
        }
        else
        {
            var subject = _mapper.Map<TeacherSubject>(newTeacherSubject);
            await _unitOfWork.TeacherSubjectRepository.AddAsync(subject);
            await _unitOfWork.SaveAsync();
        }
    }
    public async Task DeleteTeacherSubjectAsync(int id)
    {
        _unitOfWork.TeacherSubjectRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<TeacherSubjectDto>> GetAllTeacherSubjectAsync()
    {
        var list = await _unitOfWork.TeacherSubjectRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(ts => _mapper.Map<TeacherSubjectDto>(ts)).ToList();
    }

    public async Task<TeacherSubjectDto> GetTeacherSubjectByIdAsync(int id)
    {
        var teacherSubject = await _unitOfWork.TeacherSubjectRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<TeacherSubjectDto>(teacherSubject);
    }

    public async Task UpdateTeacherSubjectAsync(TeacherSubjectDto teacherSubjectDto)
    {
        var teacherSubject = _mapper.Map<TeacherSubject>(teacherSubjectDto);
        _unitOfWork.TeacherSubjectRepository.Update(teacherSubject); 
        await _unitOfWork.SaveAsync();
    }

}
