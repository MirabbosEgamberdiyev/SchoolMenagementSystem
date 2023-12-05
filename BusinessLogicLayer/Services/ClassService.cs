

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.ClassDtos;

namespace BusinessLogicLayer.Services;

public class ClassService : IClassService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClassService(IUnitOfWork unitOfWork,
                        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddClassAsync(AddClassDto newClass)
    {
        if (newClass == null)
        {
            throw new ArgumentNullException(nameof(newClass), "New Class is null");
        }

        if (string.IsNullOrEmpty(newClass.ClassName))
        {
            throw new ArgumentNullException(nameof(newClass.ClassName), "Class name is required!");
        }

        var allClasses = await _unitOfWork.ClassRepository.GetAllAsync();

        if (allClasses.Any(c => c.ClassName == newClass.ClassName))
        {
            throw new InvalidOperationException("Class name is already exist!");
        }
        else
        {
            var newClassEntity = _mapper.Map<Class>(newClass);
            await _unitOfWork.ClassRepository.AddAsync(newClassEntity);
            await _unitOfWork.SaveAsync();
        }
    }


    public async Task DeleteClassAsync(int id)
    {
      _unitOfWork.ClassRepository.Delete(id);
       await _unitOfWork.SaveAsync();
    }

    public async Task<List<ClassDto>> GetAllClassAsync()
    {
       var list =  await _unitOfWork.ClassRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(c => _mapper.Map<ClassDto>(c)).ToList();
        
    }

    public async Task<ClassDto> GetClassByIdAsync(int id)
    {
       var clas = await _unitOfWork.ClassRepository.GetByIdAsync(id);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<ClassDto>(clas);
    }

    public async Task UpdateClassAsync(ClassDto classDto)
    {
        if (classDto == null) {
            throw new ArgumentNullException("Class is null");
        }
        else
        {
            var clas = _mapper.Map<Class>(classDto);
            _unitOfWork.ClassRepository.Update(clas);
            await _unitOfWork.SaveAsync();
        }
    }
}
