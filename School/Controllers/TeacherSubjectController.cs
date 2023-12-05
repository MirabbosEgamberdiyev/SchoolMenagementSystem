using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.TeacherSubjectDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly ITeacherSubjectService _teacherSubjectService;
        private readonly ILoggingService _loggingService;

        public TeacherSubjectController(ITeacherSubjectService teacherSubjectService, ILoggingService loggingService)
        {
            _teacherSubjectService = teacherSubjectService ?? throw new ArgumentNullException(nameof(teacherSubjectService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TeacherSubjectDto>>> GetAllTeacherSubjects()
        {
            try
            {
                var teacherSubjects = await _teacherSubjectService.GetAllTeacherSubjectAsync();
                return Ok(teacherSubjects);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllTeacherSubjects method: {ex.Message}");
                return BadRequest("Something went wrong while fetching teacher subjects.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<TeacherSubjectDto>> GetTeacherSubjectById(int id)
        {
            try
            {
                var teacherSubject = await _teacherSubjectService.GetTeacherSubjectByIdAsync(id);

                if (teacherSubject == null)
                {
                    return NotFound("Teacher subject not found.");
                }

                return Ok(teacherSubject);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetTeacherSubjectById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the teacher subject.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TeacherSubjectDto>> AddTeacherSubject(AddTeacherSubjectDto newTeacherSubject)
        {
            try
            {
                var addedTeacherSubject =  _teacherSubjectService.AddTeacherSubjectAsync(newTeacherSubject);
                _loggingService.LogInfo("New teacher subject added successfully.");
                return CreatedAtAction(nameof(GetTeacherSubjectById), new { id = addedTeacherSubject.Id }, addedTeacherSubject);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddTeacherSubject method: {ex.Message}");
                return BadRequest("Something went wrong while adding the teacher subject.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateTeacherSubject(int id, TeacherSubjectDto teacherSubjectDto)
        {
            try
            {
                var existingTeacherSubject = await _teacherSubjectService.GetTeacherSubjectByIdAsync(id);

                if (existingTeacherSubject == null)
                {
                    return NotFound("Teacher subject not found.");
                }

                if (id != teacherSubjectDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _teacherSubjectService.UpdateTeacherSubjectAsync(teacherSubjectDto);
                _loggingService.LogInfo($"Teacher subject with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateTeacherSubject method: {ex.Message}");
                return BadRequest("Something went wrong while updating the teacher subject.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacherSubject(int id)
        {
            try
            {
                var existingTeacherSubject = await _teacherSubjectService.GetTeacherSubjectByIdAsync(id);

                if (existingTeacherSubject == null)
                {
                    return NotFound("Teacher subject not found.");
                }

                await _teacherSubjectService.DeleteTeacherSubjectAsync(id);
                _loggingService.LogInfo($"Teacher subject with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteTeacherSubject method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the teacher subject.");
            }
        }
    }
}
