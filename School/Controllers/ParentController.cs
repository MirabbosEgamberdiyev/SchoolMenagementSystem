using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.ParentDtos;
using BusinessLogicLayer.Helpers;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;
        private readonly ILoggingService _loggingService;

        public ParentController(IParentService parentService, ILoggingService loggingService)
        {
            _parentService = parentService;
            _loggingService = loggingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetAllParents()
        {
            try
            {
                var parents = await _parentService.GetAllParentsAsync();
                return Ok(parents);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllParents method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ParentDto>> GetParentById(int id)
        {
            try
            {
                var parent = await _parentService.GetParentByIdAsync(id);

                if (parent == null)
                {
                    return NotFound();
                }

                return Ok(parent);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetParentById method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ParentDto>> AddParent(AddParentDto newParent)
        {
            try
            {
                await _parentService.AddParentAsync(newParent);
                _loggingService.LogInfo("New parent added successfully.");
                return Ok(newParent);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddParent method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateParent(int id, ParentDto parentDto)
        {
            try
            {
                var existingParent = await _parentService.GetParentByIdAsync(id);

                if (existingParent == null)
                {
                    return NotFound();
                }

                if (id != parentDto.Id)
                {
                    return BadRequest();
                }

                await _parentService.UpdateParentAsync(parentDto);
                _loggingService.LogInfo($"Parent with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateParent method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            try
            {
                var existingParent = await _parentService.GetParentByIdAsync(id);

                if (existingParent == null)
                {
                    return NotFound();
                }

                await _parentService.DeleteParentAsync(id);
                _loggingService.LogInfo($"Parent with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteParent method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
