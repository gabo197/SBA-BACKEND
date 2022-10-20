using SBA_BACKEND.Domain.Models;

namespace SBA_BACKEND.Domain.Services.Communications
{
    public class AppointmentResponse : BaseResponse<Appointment>
    {
        public AppointmentResponse(Appointment resource) : base(resource)
        {
        }

        public AppointmentResponse(string message) : base(message)
        {
        }
    }
}
