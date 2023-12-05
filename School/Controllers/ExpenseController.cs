using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Interfaces;
using SchoolApi.Dto.ExpenseDtos;
using BusinessLogicLayer.Helpers;


namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ILoggingService _loggingService;

        public ExpenseController(IExpenseService expenseService, ILoggingService loggingService)
        {
            _expenseService = expenseService;
            _loggingService = loggingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetExpenseAsync();
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetAllExpenses method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpenseById(int id)
        {
            try
            {
                var expense = await _expenseService.GetExpenseByIdAsync(id);

                if (expense == null)
                {
                    return NotFound();
                }

                return Ok(expense);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in GetExpenseById method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ExpenseDto>> AddExpense(AddExpenseDto newExpense)
        {
            try
            {
                await _expenseService.AddExpenseAsync(newExpense);
                _loggingService.LogInfo("New expense added successfully.");
                return Ok(newExpense);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in AddExpense method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseDto expenseDto)
        {
            try
            {
                var existingExpense = await _expenseService.GetExpenseByIdAsync(id);

                if (existingExpense == null)
                {
                    return NotFound();
                }

                if (id != expenseDto.Id)
                {
                    return BadRequest();
                }

                await _expenseService.UpdateExpenseAsync(expenseDto);
                _loggingService.LogInfo($"Expense with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in UpdateExpense method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                var existingExpense = await _expenseService.GetExpenseByIdAsync(id);

                if (existingExpense == null)
                {
                    return NotFound();
                }

                await _expenseService.DeleteExpenseAsync(id);
                _loggingService.LogInfo($"Expense with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Error in DeleteExpense method: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
