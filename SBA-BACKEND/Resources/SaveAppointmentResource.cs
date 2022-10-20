using System;

namespace SBA_BACKEND.Resources
{
    public class SaveAppointmentResource
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Valorization { get; set; }
    }
}
