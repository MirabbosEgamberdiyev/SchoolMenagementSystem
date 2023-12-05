
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
