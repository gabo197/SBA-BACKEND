using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        public PaymentMethodService(IPaymentMethodRepository appointmentRepository)
        {
            _paymentMethodRepository = appointmentRepository;
        }
        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _paymentMethodRepository.ListAsync();
        }
    }
}
