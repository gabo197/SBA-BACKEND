using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Customer.Customer.API.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
    }
}
