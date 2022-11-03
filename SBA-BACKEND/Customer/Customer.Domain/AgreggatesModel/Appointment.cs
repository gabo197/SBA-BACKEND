using System;

namespace SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int TechnicianId { get; set; }
        public Technician.Technician.Domain.AgreggatesModel.Technician Technician { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Valorization { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }

    }
}
