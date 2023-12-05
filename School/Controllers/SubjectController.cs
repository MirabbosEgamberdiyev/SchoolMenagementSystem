using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.SubjectDtos;
using BusinessLogicLayer.Helpers;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILoggingService _loggingService;

        public SubjectController(ISubjectService subjectService, ILoggingService loggingService)
        {
            _subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjectAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllSubjects method: {ex.Message}");
                return BadRequest("Something went wrong while fetching subjects.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);

                if (subject == null)
                {
                    return NotFound("Subject not found.");
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetSubjectById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the subject.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubjectDto>> AddSubject(AddSubjectDto newSubject)
        {
            try
            {
                var addedSubject =  _subjectService.AddSubjectAsync(newSubject);
                _loggingService.LogInfo("New subject added successfully.");
                return CreatedAtAction(nameof(GetSubjectById), new { id = addedSubject.Id }, addedSubject);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddSubject method: {ex.Message}");
                return BadRequest("Something went wrong while adding the subject.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectDto subjectDto)
        {
            try
            {
                var existingSubject = await _subjectService.GetSubjectByIdAsync(id);

                if (existingSubject == null)
                {
                    return NotFound("Subject not found.");
                }

                if (id != subjectDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _subjectService.UpdateSubjectAsync(subjectDto);
                _loggingService.LogInfo($"Subject with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateSubject method: {ex.Message}");
                return BadRequest("Something went wrong while updating the subject.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var existingSubject = await _subjectService.GetSubjectByIdAsync(id);

                if (existingSubject == null)
                {
                    return NotFound("Subject not found.");
                }

                await _subjectService.DeleteSubjectAsync(id);
                _loggingService.LogInfo($"Subject with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteSubject method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the subject.");
            }
        }
    }
}
