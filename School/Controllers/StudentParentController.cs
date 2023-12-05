using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.StudentParentDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentParentController : ControllerBase
    {
        private readonly IStudentParentService _studentParentService;
        private readonly ILoggingService _loggingService;

        public StudentParentController(IStudentParentService studentParentService, ILoggingService loggingService)
        {
            _studentParentService = studentParentService ?? throw new ArgumentNullException(nameof(studentParentService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<StudentParentDto>>> GetAllStudentParents()
        {
            try
            {
                var studentParents = await _studentParentService.GetAllStudentParentsAsync();
                return Ok(studentParents);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllStudentParents method: {ex.Message}");
                return BadRequest("Something went wrong while fetching student-parent relationships.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<StudentParentDto>> GetStudentParentById(int id)
        {
            try
            {
                var studentParent = await _studentParentService.GetStudentParentByIdAsync(id);

                if (studentParent == null)
                {
                    return NotFound("Student-parent relationship not found.");
                }

                return Ok(studentParent);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetStudentParentById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the student-parent relationship.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<StudentParentDto>> AddStudentParent(AddStudentParentDto newStudentParent)
        {
            try
            {
                var addedStudentParent =  _studentParentService.AddStudentParentAsync(newStudentParent);
                _loggingService.LogInfo("New student-parent relationship added successfully.");
                return CreatedAtAction(nameof(GetStudentParentById), new { id = addedStudentParent.Id }, addedStudentParent);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddStudentParent method: {ex.Message}");
                return BadRequest("Something went wrong while adding the student-parent relationship.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateStudentParent(int id, StudentParentDto studentParentDto)
        {
            try
            {
                var existingStudentParent = await _studentParentService.GetStudentParentByIdAsync(id);

                if (existingStudentParent == null)
                {
                    return NotFound("Student-parent relationship not found.");
                }

                if (id != studentParentDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _studentParentService.UpdateStudentParentAsync(studentParentDto);
                _loggingService.LogInfo($"Student-parent relationship with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateStudentParent method: {ex.Message}");
                return BadRequest("Something went wrong while updating the student-parent relationship.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudentParent(int id)
        {
            try
            {
                var existingStudentParent = await _studentParentService.GetStudentParentByIdAsync(id);

                if (existingStudentParent == null)
                {
                    return NotFound("Student-parent relationship not found.");
                }

                await _studentParentService.DeleteStudentParentAsync(id);
                _loggingService.LogInfo($"Student-parent relationship with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteStudentParent method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the student-parent relationship.");
            }
        }
    }
}
