using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.StudentAttendanceDtos;
using BusinessLogicLayer.Helpers;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly ILoggingService _loggingService;

        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService, ILoggingService loggingService)
        {
            _studentAttendanceService = studentAttendanceService ?? throw new ArgumentNullException(nameof(studentAttendanceService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<StudentAttendanceDto>>> GetAllStudentAttendances()
        {
            try
            {
                var studentAttendances = await _studentAttendanceService.GetAllStudentAttendancesAsync();
                return Ok(studentAttendances);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllStudentAttendances method: {ex.Message}");
                return BadRequest("Something went wrong while fetching student attendances.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<StudentAttendanceDto>> GetStudentAttendanceById(int id)
        {
            try
            {
                var studentAttendance = await _studentAttendanceService.GetStudentAttendanceByIdAsync(id);

                if (studentAttendance == null)
                {
                    return NotFound("Student attendance not found.");
                }

                return Ok(studentAttendance);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetStudentAttendanceById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the student attendance.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<StudentAttendanceDto>> AddStudentAttendance(AddStudentAttendanceDto newStudentAttendance)
        {
            try
            {
                var addedStudentAttendance =  _studentAttendanceService.AddStudentAttendanceAsync(newStudentAttendance);
                _loggingService.LogInfo("New student attendance added successfully.");
                return CreatedAtAction(nameof(GetStudentAttendanceById), new { id = addedStudentAttendance.Id }, addedStudentAttendance);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddStudentAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while adding the student attendance.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateStudentAttendance(int id, StudentAttendanceDto studentAttendanceDto)
        {
            try
            {
                var existingStudentAttendance = await _studentAttendanceService.GetStudentAttendanceByIdAsync(id);

                if (existingStudentAttendance == null)
                {
                    return NotFound("Student attendance not found.");
                }

                if (id != studentAttendanceDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _studentAttendanceService.UpdateStudentAttendanceAsync(studentAttendanceDto);
                _loggingService.LogInfo($"Student attendance with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateStudentAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while updating the student attendance.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudentAttendance(int id)
        {
            try
            {
                var existingStudentAttendance = await _studentAttendanceService.GetStudentAttendanceByIdAsync(id);

                if (existingStudentAttendance == null)
                {
                    return NotFound("Student attendance not found.");
                }

                await _studentAttendanceService.DeleteStudentAttendanceAsync(id);
                _loggingService.LogInfo($"Student attendance with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteStudentAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the student attendance.");
            }
        }
    }
}
