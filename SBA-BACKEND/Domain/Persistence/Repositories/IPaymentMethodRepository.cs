using SBA_BACKEND.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Persistence.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
    }
}
