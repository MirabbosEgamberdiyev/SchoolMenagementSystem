using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Dto.ExamDtos;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly ILoggingService _loggingService;

        public ExamController(IExamService examService, ILoggingService loggingService)
        {
            _examService = examService;
            _loggingService = loggingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetAllExams()
        {
            try
            {
                var exams = await _examService.GetExamsAsync();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllExams method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ExamDto>> GetExamById(int id)
        {
            try
            {
                var examInfo = await _examService.GetExamByIdAsync(id);

                if (examInfo == null)
                {
                    return NotFound();
                }

                return Ok(examInfo);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetExamById method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ExamDto>> AddExam(AddExamDto newExam)
        {
            try
            {
                await _examService.AddExamAsync(newExam);
                _loggingService.LogInfo("New exam added successfully.");
                return Ok(newExam);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddExam method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateExam(int id, ExamDto examDto)
        {
            try
            {
                var existingExam = await _examService.GetExamByIdAsync(id);

                if (existingExam == null)
                {
                    return NotFound();
                }

                if (id != examDto.Id)
                {
                    return BadRequest();
                }

                await _examService.UpdateExamAsync(examDto);
                _loggingService.LogInfo($"Exam with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateExam method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                var existingExam = await _examService.GetExamByIdAsync(id);

                if (existingExam == null)
                {
                    return NotFound();
                }

                await _examService.DeleteExamAsync(id);
                _loggingService.LogInfo($"Exam with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteExam method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
