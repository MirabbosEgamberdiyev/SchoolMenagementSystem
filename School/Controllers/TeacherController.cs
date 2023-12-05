using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.TeacherDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILoggingService _loggingService;

        public TeacherController(ITeacherService teacherService, ILoggingService loggingService)
        {
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeachers()
        {
            try
            {
                var teachers = await _teacherService.GetAllTeacherAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllTeachers method: {ex.Message}");
                return BadRequest("Something went wrong while fetching teachers.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);

                if (teacher == null)
                {
                    return NotFound("Teacher not found.");
                }

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetTeacherById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the teacher.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TeacherDto>> AddTeacher(AddTeacherDto newTeacher)
        {
            try
            {
                var addedTeacher =  _teacherService.AddTeacherAsync(newTeacher);
                _loggingService.LogInfo("New teacher added successfully.");
                return CreatedAtAction(nameof(GetTeacherById), new { id = addedTeacher.Id }, addedTeacher);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddTeacher method: {ex.Message}");
                return BadRequest("Something went wrong while adding the teacher.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherDto teacherDto)
        {
            try
            {
                var existingTeacher = await _teacherService.GetTeacherByIdAsync(id);

                if (existingTeacher == null)
                {
                    return NotFound("Teacher not found.");
                }

                if (id != teacherDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _teacherService.UpdateTeacherAsync(teacherDto);
                _loggingService.LogInfo($"Teacher with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateTeacher method: {ex.Message}");
                return BadRequest("Something went wrong while updating the teacher.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var existingTeacher = await _teacherService.GetTeacherByIdAsync(id);

                if (existingTeacher == null)
                {
                    return NotFound("Teacher not found.");
                }

                await _teacherService.DeleteTeacherAsync(id);
                _loggingService.LogInfo($"Teacher with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteTeacher method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the teacher.");
            }
        }
    }
}
