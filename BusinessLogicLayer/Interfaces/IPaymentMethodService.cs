

using SchoolApi.Dto.PaymentMethodDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IPaymentMethodService
{
    Task<List<PaymentMethodDto>> GetAllPaymentMethodAsync();
    Task<PaymentMethodDto> GetPaymentMethodByIdAsync(int id);
    Task AddPaymentMethodAsync(AddPaymentMethodDto newPaymentMethod);
    Task UpdatePaymentMethodAsync(PaymentMethodDto paymentMethodDto);
    Task DeletePaymentMethodAsync(int id);
}
