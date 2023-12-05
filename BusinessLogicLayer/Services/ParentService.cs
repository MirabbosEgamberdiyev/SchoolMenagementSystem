
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.ParentDtos;

namespace BusinessLogicLayer.Services;

public class ParentService : IParentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ParentService(IUnitOfWork unitOfWork,
                         IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddParentAsync(AddParentDto parentDto)
    {
       if (parentDto == null)
        {
            throw new ArgumentNullException(nameof(parentDto) ,"Parent is null here");
        }
       if( string.IsNullOrEmpty(parentDto.FirstName))
        {
            throw new ArgumentException("Name is required");
        }
       var list = await _unitOfWork.ParentRepository.GetAllAsync();
        if(list.Any(p => p.LastName == parentDto.LastName && p.FirstName == parentDto.FirstName))
        {
            throw new ArgumentException("Parent name is already exist");
        }
        else
        {
            var parent = _mapper.Map<Parent>(parentDto);
            await _unitOfWork.ParentRepository.AddAsync(parent);
            await _unitOfWork.SaveAsync();
        }
       
    }

    public async Task DeleteParentAsync(int id)
    {
        _unitOfWork.ParentRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<ParentDto>> GetAllParentsAsync()
    {
      var list = await _unitOfWork.ParentRepository.GetAllAsync();
      return list.Select(p =>  _mapper.Map<ParentDto>(p)).ToList();
    }

    public async Task<ParentDto> GetParentByIdAsync(int id)
    {
        var parent = await _unitOfWork.ParentRepository.GetByIdAsync(id);
        return _mapper.Map<ParentDto>(parent);
    }

    public async Task UpdateParentAsync(ParentDto parentDto)
    {
        var parent = _mapper.Map<Parent>(parentDto);
        _unitOfWork.ParentRepository.Update(parent);
        await _unitOfWork.SaveAsync();
    }
}
