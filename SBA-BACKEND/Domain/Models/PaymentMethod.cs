using System.Collections.Generic;

namespace SBA_BACKEND.Domain.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
