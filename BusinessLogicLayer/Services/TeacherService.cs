

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.TeacherDtos;

namespace BusinessLogicLayer.Services;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeacherService(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddTeacherAsync(AddTeacherDto newTeacher)
    {
        if(newTeacher == null)
        {
            throw new ArgumentNullException(nameof(newTeacher), "Teacher is null here ");
        }
        if (string.IsNullOrEmpty(newTeacher.FirstName))
        {
            throw new ArgumentException("Teacher name is required");
        }
        var list = await _unitOfWork.TeacherRepository.GetAllAsync();
        if(list.Any(t => t.FirstName == newTeacher.FirstName && t.LastName==newTeacher.LastName && t.DOB==newTeacher.DOB))
        {
            throw new Exception("Teacher is already exist");
        }
        else
        {
            var teacher = _mapper.Map<Teacher>(newTeacher);
            await _unitOfWork.TeacherRepository.AddAsync(teacher);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteTeacherAsync(int id)
    {
        _unitOfWork.TeacherRepository.Delete(id);
        await  _unitOfWork.SaveAsync();
    }

    public async Task<List<TeacherDto>> GetAllTeacherAsync()
    {
        var list = await _unitOfWork.TeacherRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(t => _mapper.Map<TeacherDto>(t)).ToList();   
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(int id)
    {
        var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<TeacherDto>(teacher);
    }

    public async Task UpdateTeacherAsync(TeacherDto teacherDto)
    {
        var teacher = _mapper.Map<Teacher>(teacherDto);
        _unitOfWork.TeacherRepository.Update(teacher);
        await _unitOfWork.SaveAsync();
    }
}
