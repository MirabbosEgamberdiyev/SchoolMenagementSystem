
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.StudentDtos;

namespace BusinessLogicLayer.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudentService(IUnitOfWork unitOfWork,
                          IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddStudentAsync(AddStudentDto studentDto)
    {
        if (studentDto == null) throw new ArgumentNullException(nameof(studentDto) , "Student is null here");
        if (string.IsNullOrEmpty(studentDto.FirstName))
        {
            throw new ArgumentNullException("Student name is required");
        }
        var students = await _unitOfWork.StudentRepository.GetAllAsync();
        if(students.Any(s => s.FirsName == studentDto.FirstName && s.LastName==studentDto.LastName && s.DOB==studentDto.DOB))
        {
            throw new ArgumentException("Student name is already exist");
        }
        else
        {
            var student = _mapper.Map<Student>(studentDto);
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteStudentAsync(int id)
    {
        _unitOfWork.StudentRepository.Delete(id);
        await _unitOfWork.SaveAsync();  
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var list = await _unitOfWork.StudentRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(s => _mapper.Map<StudentDto>(s)).ToList();   

    }

    public async Task<StudentDto> GetStudentByIdAsync(int id)
    {
        var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<StudentDto>(student);
    }

    public async Task UpdateStudentAsync(StudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        _unitOfWork.StudentRepository.Update(student);
        await _unitOfWork.SaveAsync();
    }
}
