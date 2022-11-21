using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Technician.Technician.API.Services.Communications;

namespace SBA_BACKEND.Technician.Technician.API.Services
{
    public interface ITechnicianService
    {
        Task<IEnumerable<Technician.Domain.AgreggatesModel.Technician>> ListAsync();
        Task<IEnumerable<Technician.Domain.AgreggatesModel.Technician>> ListTechniciansBySpecialtyId(int specialtyId);
        Task<TechnicianResponse> GetByIdAsync(int id);
        Task<TechnicianResponse> SaveAsync(int userId, Technician.Domain.AgreggatesModel.Technician technician);
        Task<TechnicianResponse> UpdateAsync(int id, Technician.Domain.AgreggatesModel.Technician technician);
        Task<TechnicianResponse> DeleteAsync(int id);

    }
}
