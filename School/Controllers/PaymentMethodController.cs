using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.PaymentMethodDtos;
using BusinessLogicLayer.Helpers;


namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ILoggingService _loggingService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService, ILoggingService loggingService)
        {
            _paymentMethodService = paymentMethodService ?? throw new ArgumentNullException(nameof(paymentMethodService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethods()
        {
            try
            {
                var paymentMethods = await _paymentMethodService.GetAllPaymentMethodAsync();
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllPaymentMethods method: {ex.Message}");
                return BadRequest("Something went wrong while fetching payment methods.");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<PaymentMethodDto>> GetPaymentMethodById(int id)
        {
            try
            {
                var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);

                if (paymentMethod == null)
                {
                    return NotFound("Payment method not found.");
                }

                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetPaymentMethodById method: {ex.Message}");
                return BadRequest("Something went wrong while fetching the payment method.");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<PaymentMethodDto>> AddPaymentMethod(AddPaymentMethodDto newPaymentMethod)
        {
            try
            {
                var addedPaymentMethod =  _paymentMethodService.AddPaymentMethodAsync(newPaymentMethod);
                _loggingService.LogInfo("New payment method added successfully.");
                return CreatedAtAction(nameof(GetPaymentMethodById), new { id = addedPaymentMethod.Id }, addedPaymentMethod);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddPaymentMethod method: {ex.Message}");
                return BadRequest("Something went wrong while adding the payment method.");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, PaymentMethodDto paymentMethodDto)
        {
            try
            {
                var existingPaymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);

                if (existingPaymentMethod == null)
                {
                    return NotFound("Payment method not found.");
                }

                if (id != paymentMethodDto.Id)
                {
                    return BadRequest("Invalid request.");
                }

                await _paymentMethodService.UpdatePaymentMethodAsync(paymentMethodDto);
                _loggingService.LogInfo($"Payment method with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdatePaymentMethod method: {ex.Message}");
                return BadRequest("Something went wrong while updating the payment method.");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            try
            {
                var existingPaymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);

                if (existingPaymentMethod == null)
                {
                    return NotFound("Payment method not found.");
                }

                await _paymentMethodService.DeletePaymentMethodAsync(id);
                _loggingService.LogInfo($"Payment method with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeletePaymentMethod method: {ex.Message}");
                return BadRequest("Something went wrong while deleting the payment method.");
            }
        }
    }
}
