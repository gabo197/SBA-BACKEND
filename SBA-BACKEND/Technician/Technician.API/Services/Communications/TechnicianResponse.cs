using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Technician.Technician.API.Services.Communications
{
    public class TechnicianResponse : BaseResponse<Technician.Domain.AgreggatesModel.Technician>
    {
        public TechnicianResponse(Technician.Domain.AgreggatesModel.Technician resource) : base(resource)
        {
        }

        public TechnicianResponse(string message) : base(message)
        {
        }
    }
}
