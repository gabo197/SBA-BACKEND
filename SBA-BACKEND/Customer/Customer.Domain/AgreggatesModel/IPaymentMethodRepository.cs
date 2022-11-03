using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
    }
}
