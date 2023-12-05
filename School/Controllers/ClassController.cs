using Microsoft.AspNetCore.Mvc;
using SchoolApi.Dto.ClassDtos;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Helpers;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ILoggingService _loggingService;

        public ClassController(IClassService classService, ILoggingService loggingService)
        {
            _classService = classService;
            _loggingService = loggingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAllClasses()
        {
            try
            {
                var classes = await _classService.GetAllClassAsync();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllClasses method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ClassDto>> GetClassById(int id)
        {
            try
            {
                var classInfo = await _classService.GetClassByIdAsync(id);

                if (classInfo == null)
                {
                    return NotFound();
                }

                return Ok(classInfo);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetClassById method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ClassDto>> AddClass(AddClassDto newClass)
        {
            try
            {
                await _classService.AddClassAsync(newClass);
                _loggingService.LogInfo("New class added successfully.");
                return Ok(newClass);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddClass method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateClass(int id, ClassDto classDto)
        {
            try
            {
                var existingClass = await _classService.GetClassByIdAsync(id);

                if (existingClass == null)
                {
                    return NotFound();
                }

                if (id != classDto.Id)
                {
                    return BadRequest();
                }

                await _classService.UpdateClassAsync(classDto);
                _loggingService.LogInfo($"Class with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateClass method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                var existingClass = await _classService.GetClassByIdAsync(id);

                if (existingClass == null)
                {
                    return NotFound();
                }

                await _classService.DeleteClassAsync(id);
                _loggingService.LogInfo($"Class with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteClass method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
