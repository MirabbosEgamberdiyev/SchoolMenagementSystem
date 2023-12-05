


using SchoolApi.Dto.ExpenseDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetExpenseAsync();
    Task<ExpenseDto> GetExpenseByIdAsync(int id);
    Task AddExpenseAsync(AddExpenseDto addExpense);
    Task UpdateExpenseAsync(ExpenseDto expenseDto);
    Task DeleteExpenseAsync(int id);
    
}
