using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Customer.Customer.API.Services.Communications
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
