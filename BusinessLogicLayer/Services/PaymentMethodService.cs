

using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using SchoolApi.Dto.PaymentMethodDtos;

namespace BusinessLogicLayer.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaymentMethodService(IUnitOfWork unitOfWork,
                                IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddPaymentMethodAsync(AddPaymentMethodDto newPaymentMethod)
    {
      if (newPaymentMethod == null)
        {
            throw new ArgumentNullException(nameof(newPaymentMethod), "New Payment Method is null here ");
        }
      if(string.IsNullOrEmpty(newPaymentMethod.MethodName))
        {
            throw new NullReferenceException("Method is null here");
        }
      var list = await _unitOfWork.PaymentMethodRepository.GetAllAsync();
      if(list.Any(p => p.MethodName == newPaymentMethod.MethodName))
        {
            throw new ArgumentException("MethodName is already exist");
        }
        else
        {
            var method = _mapper.Map<PaymentMethod>(newPaymentMethod);
            await _unitOfWork.PaymentMethodRepository.AddAsync(method);
            await _unitOfWork.SaveAsync();
        }
    }



    public async Task DeletePaymentMethodAsync(int id)
    {
         _unitOfWork.PaymentMethodRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<PaymentMethodDto>> GetAllPaymentMethodAsync()
    {
        var list = await _unitOfWork.PaymentMethodRepository.GetAllAsync();
        await _unitOfWork.SaveAsync();
        return list.Select(p => _mapper.Map<PaymentMethodDto>(p)).ToList();
    }

    public async Task<PaymentMethodDto> GetPaymentMethodByIdAsync(int id)
    {
        var paymentMethod = await _unitOfWork.PaymentMethodRepository.GetByIdAsync(id);
        await  _unitOfWork.SaveAsync();
        return _mapper.Map<PaymentMethodDto>(paymentMethod);
    }

    public async Task UpdatePaymentMethodAsync(PaymentMethodDto paymentMethodDto)
    {
        var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
        _unitOfWork.PaymentMethodRepository.Update(paymentMethod);
        await _unitOfWork.SaveAsync();
    }


}
