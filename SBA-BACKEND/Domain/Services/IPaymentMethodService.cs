using SBA_BACKEND.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
    }
}
