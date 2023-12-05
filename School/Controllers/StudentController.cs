using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.StudentDtos;
using BusinessLogicLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILoggingService _loggingService;

        public StudentController(IStudentService studentService, ILoggingService loggingService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllStudents method: {ex.Message}");
                return BadRequest("Something went wrong while fetching students.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);

                if (student == null)
                {
                    return NotFound("Student not found.");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetStudentById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the student.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<StudentDto>> AddStudent(AddStudentDto newStudent)
        {
            try
            {
                var addedStudent =  _studentService.AddStudentAsync(newStudent);
                _loggingService.LogInfo("New student added successfully.");
                return CreatedAtAction(nameof(GetStudentById), new { id = addedStudent.Id }, addedStudent);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddStudent method: {ex.Message}");
                return BadRequest("Something went wrong while adding the student.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            try
            {
                var existingStudent = await _studentService.GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return NotFound("Student not found.");
                }

                if (id != studentDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _studentService.UpdateStudentAsync(studentDto);
                _loggingService.LogInfo($"Student with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateStudent method: {ex.Message}");
                return BadRequest("Something went wrong while updating the student.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var existingStudent = await _studentService.GetStudentByIdAsync(id);

                if (existingStudent == null)
                {
                    return NotFound("Student not found.");
                }

                await _studentService.DeleteStudentAsync(id);
                _loggingService.LogInfo($"Student with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteStudent method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the student.");
            }
        }
    }
}
