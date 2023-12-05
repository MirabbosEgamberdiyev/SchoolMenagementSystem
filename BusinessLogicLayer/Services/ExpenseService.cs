
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.ExpenseDtos;

namespace BusinessLogicLayer.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork unitOfWork,
                              IMapper mapper    )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddExpenseAsync(AddExpenseDto addExpense)
        {
          if ( addExpense == null )
            {
                throw new ArgumentNullException("Expense is null here");
            }
          var newExpenseDto =  _mapper.Map<Expense>( addExpense );
          await  _unitOfWork.ExpenseRepository.AddAsync(newExpenseDto);
          await _unitOfWork.SaveAsync();


        }

        public async Task DeleteExpenseAsync(int id)
        {
           _unitOfWork.ExpenseRepository.Delete(id);
           await _unitOfWork.SaveAsync();
        }

        public async Task<List<ExpenseDto>> GetExpenseAsync()
        {
            var list = await _unitOfWork.ExpenseRepository.GetAllAsync();
            await _unitOfWork.SaveAsync();
            return list.Select(c => _mapper.Map<ExpenseDto>(c)).ToList();
        }

        public async Task<ExpenseDto> GetExpenseByIdAsync(int id)
        {
            var expense =  await _unitOfWork.ExamRepository.GetByIdAsync(id);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task UpdateExpenseAsync(ExpenseDto expenseDto)
        {
            if(expenseDto == null)
            {
                throw new ArgumentNullException("ExpenseDto is null");
            }
            else
            {
                var expense =   _mapper.Map<Expense>(expenseDto);
                _unitOfWork.ExpenseRepository.Update(expense);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
