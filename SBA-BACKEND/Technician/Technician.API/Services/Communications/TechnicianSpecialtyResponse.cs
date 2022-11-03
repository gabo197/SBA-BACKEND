using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;

namespace SBA_BACKEND.Technician.Technician.API.Services.Communications
{
    public class TechnicianSpecialtyResponse : BaseResponse<TechnicianSpecialty>
    {
        public TechnicianSpecialtyResponse(TechnicianSpecialty resource) : base(resource)
        {
        }

        public TechnicianSpecialtyResponse(string message) : base(message)
        {
        }
    }
}
