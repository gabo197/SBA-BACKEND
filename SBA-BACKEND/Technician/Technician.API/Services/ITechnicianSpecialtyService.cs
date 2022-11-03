using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Technician.Technician.API.Services.Communications;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;

namespace SBA_BACKEND.Technician.Technician.API.Services
{
    public interface ITechnicianSpecialtyService
    {
        Task<IEnumerable<TechnicianSpecialty>> ListAsync();
        Task<IEnumerable<TechnicianSpecialty>> ListByTechnicianIdAsync(int technicianId);
        Task<IEnumerable<TechnicianSpecialty>> ListBySpecialtyIdAsync(int specialtyId);
        Task<TechnicianSpecialtyResponse> AssignTechnicianSpecialtyAsync(int technicianId, int specialtyId);
        Task<TechnicianSpecialtyResponse> UnassignTechnicianSpecialtyAsync(int technicianId, int specialtyId);
    }
}
