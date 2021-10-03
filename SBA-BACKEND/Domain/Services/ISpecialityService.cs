using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Domain.Services
{
	public interface ISpecialtyService
	{
		Task<IEnumerable<Specialty>> ListAsync();
		Task<IEnumerable<Specialty>> ListByTechnicianIdAsync(int technicianId);
		Task<SpecialtyResponse> GetByIdAsync(int id);
		Task<SpecialtyResponse> SaveAsync(Specialty specialty);
		Task<SpecialtyResponse> UpdateAsync(int id, Specialty specialty);
		Task<SpecialtyResponse> DeleteAsync(int id);

	}
}
