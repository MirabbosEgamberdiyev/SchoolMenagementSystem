
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.StudentAttendanceDtos;

namespace BusinessLogicLayer.Services;

public class StudentAttendanceService : IStudentAttendanceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudentAttendanceService(IUnitOfWork unitOfWork,
                                    IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddStudentAttendanceAsync(AddStudentAttendanceDto studentAttendance)
    {
        if (studentAttendance == null)
        {
            throw new ArgumentNullException(nameof(studentAttendance), "Student attendance is null here");
        }
        var Attendance = _mapper.Map<StudentAttendance>(studentAttendance);
        await _unitOfWork.StudentAttendanceRepository.AddAsync(Attendance);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteStudentAttendanceAsync(int id)
    {
        _unitOfWork.StudentAttendanceRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<StudentAttendanceDto>> GetAllStudentAttendancesAsync()
    {
        var list = await _unitOfWork.StudentAttendanceRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(s => _mapper.Map<StudentAttendanceDto>(s)).ToList();
    }

    public async Task<StudentAttendanceDto> GetStudentAttendanceByIdAsync(int id)
    {
        var studentAttendance = await _unitOfWork.StudentAttendanceRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<StudentAttendanceDto>(studentAttendance);
    }

    public async Task UpdateStudentAttendanceAsync(StudentAttendanceDto studentAttendanceDto)
    {
        var attendance = _mapper.Map<StudentAttendance>(studentAttendanceDto);
        _unitOfWork.StudentAttendanceRepository.Update(attendance);
        await _unitOfWork.SaveAsync();
    }
}
