using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using SBA_BACKEND.Domain.Models;
 using SBA_BACKEND.Domain.Services.Communications;
 using SBA_BACKEND.Domain.Services;
 using SBA_BACKEND.Domain.Persistence.Repositories;
 
 
 namespace SBA_BACKEND.Services
 {
    public class TechnicianSpecialtyService : ITechnicianSpecialtyService
    {
        private readonly ITechnicianSpecialtyRepository technicianSpecialtyRepository;
        private readonly IUnitOfWork unitOfWork;

        public TechnicianSpecialtyService(ITechnicianSpecialtyRepository technicianSpecialtyRepository, IUnitOfWork unitOfWork)
        {
            this.technicianSpecialtyRepository = technicianSpecialtyRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TechnicianSpecialtyResponse> AssignTechnicianSpecialtyAsync(int technicianId, int specialtyId)
        {
            try
            {
                await technicianSpecialtyRepository.AssignTechnicianSpecialty(technicianId, specialtyId);
                await unitOfWork.CompleteAsync();

                TechnicianSpecialty technicianSpecialty = await technicianSpecialtyRepository.FindByTechnicianIdAndSpecialtyId(technicianId, specialtyId);

                return new TechnicianSpecialtyResponse(technicianSpecialty);
            }
            catch (Exception ex)
            {
                return new TechnicianSpecialtyResponse($"An error ocurred while assigning Specialty to Technician: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListAsync()
        {
            return await technicianSpecialtyRepository.ListAsync();
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListBySpecialtyIdAsync(int specialtyId)
        {
            return await technicianSpecialtyRepository.ListBySpecialtyIdAsync(specialtyId);
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListByTechnicianIdAsync(int technicianId)
        {
            return await technicianSpecialtyRepository.ListByTechnicianIdAsync(technicianId);
        }

        public async Task<TechnicianSpecialtyResponse> UnassignTechnicianSpecialtyAsync(int technicianId, int specialtyId)
        {
            try
            {
                TechnicianSpecialty technicianSpecialty = await technicianSpecialtyRepository.FindByTechnicianIdAndSpecialtyId(technicianId, specialtyId);
                technicianSpecialtyRepository.UnassignTechnicianSpecialty(technicianId, specialtyId);
                await unitOfWork.CompleteAsync();

                return new TechnicianSpecialtyResponse(technicianSpecialty);
            }
            catch (Exception ex)
            {
                return new TechnicianSpecialtyResponse($"An error ocurred while unassigning Specialty to Technician: {ex.Message}");
            }
        }
    }
}
