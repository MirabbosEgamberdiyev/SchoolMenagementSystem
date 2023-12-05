using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.FeesDtos;
using BusinessLogicLayer.Helpers;
namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeesService _feeService;
        private readonly ILoggingService _loggingService;

        public FeesController(IFeesService feeService, ILoggingService loggingService)
        {
            _feeService = feeService;
            _loggingService = loggingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<FeesDto>>> GetAllFees()
        {
            try
            {
                var fees = await _feeService.GetAllFeesAsync();
                return Ok(fees);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllFees method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<FeesDto>> GetFeeById(int id)
        {
            try
            {
                var fee = await _feeService.GetFeesByIdAsync(id);

                if (fee == null)
                {
                    return NotFound();
                }

                return Ok(fee);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetFeesById method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FeesDto>> AddFee(AddFeesDto newFee)
        {
            try
            {
                await _feeService.AddFeesAsync(newFee);
                _loggingService.LogInfo("New fee added successfully.");
                return Ok(newFee);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddFee method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateFee(int id, FeesDto feeDto)
        {
            try
            {
                var existingFee = await _feeService.GetFeesByIdAsync(id);

                if (existingFee == null)
                {
                    return NotFound();
                }

                if (id != feeDto.Id)
                {
                    return BadRequest();
                }

                await _feeService.UpdateFeesAsync(feeDto);
                _loggingService.LogInfo($"Fees with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateFee method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            try
            {
                var existingFee = await _feeService.GetFeesByIdAsync(id);

                if (existingFee == null)
                {
                    return NotFound();
                }

                await _feeService.DeleteFeesAsync(id);
                _loggingService.LogInfo($"Fees with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteFee method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
