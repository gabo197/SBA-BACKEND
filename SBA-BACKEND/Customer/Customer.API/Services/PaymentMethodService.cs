using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Customer.Customer.API.Services
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
