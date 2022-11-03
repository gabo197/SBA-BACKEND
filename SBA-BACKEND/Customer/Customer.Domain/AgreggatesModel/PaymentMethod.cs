using System.Collections.Generic;

namespace SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
