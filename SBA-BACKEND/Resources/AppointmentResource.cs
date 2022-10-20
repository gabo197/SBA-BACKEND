﻿using SBA_BACKEND.Domain.Models;
using System;

namespace SBA_BACKEND.Resources
{
    public class AppointmentResource
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public int TechnicianId { get; set; }
        public TechnicianResource Technician { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethodResource PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Valorization { get; set; }
    }
}