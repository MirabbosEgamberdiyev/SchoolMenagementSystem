

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.StudentParentDtos;

namespace BusinessLogicLayer.Services;

public class StudentParentService : IStudentParentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudentParentService(IUnitOfWork unitOfWork ,
                                IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddStudentParentAsync(AddStudentParentDto studentDto)
    {
        if(studentDto == null) throw new ArgumentNullException(nameof(studentDto), "Student is null here");
        var studentParent = _mapper.Map<StudentParent>(studentDto);
        await _unitOfWork.StudentParentRepository.AddAsync(studentParent);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteStudentParentAsync(int id)
    {
        _unitOfWork.StudentParentRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<StudentParentDto>> GetAllStudentParentsAsync()
    {
         var list = await _unitOfWork.StudentParentRepository.GetAllAsync();
        return list.Select(s => _mapper.Map<StudentParentDto>(s)).ToList();
    }

    public async Task<StudentParentDto> GetStudentParentByIdAsync(int id)
    {
        var studentParent = await _unitOfWork.StudentParentRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<StudentParentDto>(studentParent);
    }

    public async Task UpdateStudentParentAsync(StudentParentDto studentDto)
    {
        var studentParent = _mapper.Map<StudentParent>(studentDto);
        _unitOfWork.StudentParentRepository.Update(studentParent);
         await _unitOfWork.SaveAsync();
    }
}
