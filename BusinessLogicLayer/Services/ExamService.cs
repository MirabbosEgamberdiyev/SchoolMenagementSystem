

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.ExamDtos;

namespace BusinessLogicLayer.Services;

public class ExamService : IExamService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExamService(IUnitOfWork unitOfWork,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddExamAsync(AddExamDto newExam)
    {
        if (newExam == null)
        {
            throw new ArgumentNullException(nameof(newExam), "New Exam is null");
        }

        var allExams = await _unitOfWork.ExamRepository.GetAllAsync();

        if (allExams.Any(e => e.ClassId == newExam.ClassId))
        {
            throw new InvalidOperationException("Exam with the same class name already exists!");
        }
        else
        {
            var newExamEntity = _mapper.Map<Exam>(newExam);
            await _unitOfWork.ExamRepository.AddAsync(newExamEntity);
            await _unitOfWork.SaveAsync();
        }
    }


    public async Task DeleteExamAsync(int id)
    {
         _unitOfWork.ExamRepository.Delete(id);
        await _unitOfWork.SaveAsync();

    }

    public async Task<ExamDto> GetExamByIdAsync(int id)
    {
        var exam = await _unitOfWork.ExamRepository.GetByIdAsync(id);
        return _mapper.Map<ExamDto>(exam);  
    }

    public async Task<List<ExamDto>> GetExamsAsync()
    {
        var list = await _unitOfWork.ExamRepository.GetAllAsync();
        return list.Select(c => _mapper.Map<ExamDto>(c)).ToList();
    }

    public async Task UpdateExamAsync(ExamDto examDto)
    {
        if(examDto != null)
        {
            var exam =  _mapper.Map<Exam>(examDto);
            _unitOfWork.ExamRepository.Update(exam);
            _unitOfWork.SaveAsync();
        }
        else
        {
            throw new ArgumentNullException("Exam is null");
        };
    }
}
