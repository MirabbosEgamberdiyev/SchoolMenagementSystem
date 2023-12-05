using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.TeacherAttendanceDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAttendanceController : ControllerBase
    {
        private readonly ITeacherAttendanceService _teacherAttendanceService;
        private readonly ILoggingService _loggingService;

        public TeacherAttendanceController(ITeacherAttendanceService teacherAttendanceService, ILoggingService loggingService)
        {
            _teacherAttendanceService = teacherAttendanceService ?? throw new ArgumentNullException(nameof(teacherAttendanceService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TeacherAttendanceDto>>> GetAllTeacherAttendances()
        {
            try
            {
                var teacherAttendances = await _teacherAttendanceService.GetAllTeacherAttendanceAsync();
                return Ok(teacherAttendances);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllTeacherAttendances method: {ex.Message}");
                return BadRequest("Something went wrong while fetching teacher attendances.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<TeacherAttendanceDto>> GetTeacherAttendanceById(int id)
        {
            try
            {
                var teacherAttendance = await _teacherAttendanceService.GetTeacherAttendanceByIdAsync(id);

                if (teacherAttendance == null)
                {
                    return NotFound("Teacher attendance not found.");
                }

                return Ok(teacherAttendance);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetTeacherAttendanceById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the teacher attendance.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TeacherAttendanceDto>> AddTeacherAttendance(AddTeacherAttendanceDto newTeacherAttendance)
        {
            try
            {
                var addedTeacherAttendance =  _teacherAttendanceService.AddTeacherAttendanceAsync(newTeacherAttendance);
                _loggingService.LogInfo("New teacher attendance added successfully.");
                return CreatedAtAction(nameof(GetTeacherAttendanceById), new { id = addedTeacherAttendance.Id }, addedTeacherAttendance);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddTeacherAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while adding the teacher attendance.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateTeacherAttendance(int id, TeacherAttendanceDto teacherAttendanceDto)
        {
            try
            {
                var existingTeacherAttendance = await _teacherAttendanceService.GetTeacherAttendanceByIdAsync(id);

                if (existingTeacherAttendance == null)
                {
                    return NotFound("Teacher attendance not found.");
                }

                if (id != teacherAttendanceDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _teacherAttendanceService.UpdateTeacherAttendanceAsync(teacherAttendanceDto);
                _loggingService.LogInfo($"Teacher attendance with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateTeacherAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while updating the teacher attendance.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacherAttendance(int id)
        {
            try
            {
                var existingTeacherAttendance = await _teacherAttendanceService.GetTeacherAttendanceByIdAsync(id);

                if (existingTeacherAttendance == null)
                {
                    return NotFound("Teacher attendance not found.");
                }

                await _teacherAttendanceService.DeleteTeacherAttendanceAsync(id);
                _loggingService.LogInfo($"Teacher attendance with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteTeacherAttendance method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the teacher attendance.");
            }
        }
    }
}
