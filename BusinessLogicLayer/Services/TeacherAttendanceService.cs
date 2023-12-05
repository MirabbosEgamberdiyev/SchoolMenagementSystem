using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.TeacherAttendanceDtos;


namespace BusinessLogicLayer.Services
{
    public class TeacherAttendanceService : ITeacherAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherAttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddTeacherAttendanceAsync(AddTeacherAttendanceDto newTeacherAttendance)
        {
            ValidateAddTeacherAttendance(newTeacherAttendance);

            var existingAttendance = await _unitOfWork.TeacherAttendanceRepository
                .GetAllAsync();

            if (existingAttendance.Any())
            {
                throw new ArgumentException("Teacher attendance already exists");
            }

            var teacherAttendanceEntity = _mapper.Map<TeacherAttendance>(newTeacherAttendance);

            await _unitOfWork.TeacherAttendanceRepository.AddAsync(teacherAttendanceEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTeacherAttendanceAsync(int id)
        {
            var teacherAttendance = await _unitOfWork.TeacherAttendanceRepository.GetByIdAsync(id);

            if (teacherAttendance == null)
            {
                throw new ArgumentException("Teacher attendance not found");
            }

            _unitOfWork.TeacherAttendanceRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<TeacherAttendanceDto>> GetAllTeacherAttendanceAsync()
        {
            var teacherAttendances = await _unitOfWork.TeacherAttendanceRepository.GetAllAsync();
            return teacherAttendances.Select(t => _mapper.Map<TeacherAttendanceDto>(t)).ToList();
        }

        public async Task<TeacherAttendanceDto> GetTeacherAttendanceByIdAsync(int id)
        {
            var teacherAttendance = await _unitOfWork.TeacherAttendanceRepository.GetByIdAsync(id);

            if (teacherAttendance == null)
            {
                throw new ArgumentException("Teacher attendance not found");
            }

            return _mapper.Map<TeacherAttendanceDto>(teacherAttendance);
        }

        public async Task UpdateTeacherAttendanceAsync(TeacherAttendanceDto teacherAttendanceDto)
        {
            if (teacherAttendanceDto == null)
            {
                throw new ArgumentNullException(nameof(teacherAttendanceDto), "TeacherAttendance is null");
            }

            var teacherAttendanceEntity = _mapper.Map<TeacherAttendance>(teacherAttendanceDto);

            _unitOfWork.TeacherAttendanceRepository.Update(teacherAttendanceEntity);
            await _unitOfWork.SaveAsync();
        }

        private void ValidateAddTeacherAttendance(AddTeacherAttendanceDto newTeacherAttendance)
        {
            if (newTeacherAttendance == null)
            {
                throw new ArgumentNullException(nameof(newTeacherAttendance), "Teacher Attendance is null here");
            }

            if (newTeacherAttendance.TeacherId <= 0)
            {
                throw new ArgumentException("Invalid TeacherId");
            }
        }
    }
}
