using SBA_BACKEND.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Persistence.Repositories
{
	public interface ITechnicianSpecialtyRepository
	{
        Task<IEnumerable<TechnicianSpecialty>> ListAsync();
        Task<IEnumerable<TechnicianSpecialty>> ListByTechnicianIdAsync(int technicianId);
        Task<IEnumerable<TechnicianSpecialty>> ListBySpecialtyIdAsync(int specialtyId);
        Task<TechnicianSpecialty> FindByTechnicianIdAndSpecialtyId(int technicianId, int specialtyId);
        Task AddAsync(TechnicianSpecialty technicianSpecialty);
        void Remove(TechnicianSpecialty technicianSpecialty);
        Task AssignTechnicianSpecialty(int technicianId, int specialtyId);
        void UnassignTechnicianSpecialty(int technicianId, int specialtyId);
    }
}
