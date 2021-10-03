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
 	public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly ITechnicianSpecialtyRepository technicianSpecialtyRepository;
        private IUnitOfWork _unitOfWork;
        public SpecialtyService(ISpecialtyRepository object1, IUnitOfWork object2, ITechnicianSpecialtyRepository technicianSpecialtyRepository)
        {
            this._specialtyRepository = object1;
            this._unitOfWork = object2;
            this.technicianSpecialtyRepository = technicianSpecialtyRepository;
        }

        public async Task<SpecialtyResponse> DeleteAsync(int id)
 		{
 			var existingSpecialty = await _specialtyRepository.FindById(id);
 
 			if (existingSpecialty == null)
 				return new SpecialtyResponse("Specialty not found");
 
 			try
 			{
 				_specialtyRepository.Remove(existingSpecialty);
 				await _unitOfWork.CompleteAsync();
 
 				return new SpecialtyResponse(existingSpecialty);
 			}
 			catch (Exception ex)
 			{
 				return new SpecialtyResponse($"An error ocurred while deleting the specialty: {ex.Message}");
 			}
 		}
 
 		public async Task<SpecialtyResponse> GetByIdAsync(int id)
 		{
 			var existingSpecialty = await _specialtyRepository.FindById(id);
 
 			if (existingSpecialty == null)
 				return new SpecialtyResponse("Specialty not found");
 			return new SpecialtyResponse(existingSpecialty);
 		}
 
 		public async Task<IEnumerable<Specialty>> ListAsync()
 		{
 			return await _specialtyRepository.ListAsync();
 		}

        public async Task<IEnumerable<Specialty>> ListByTechnicianIdAsync(int technicianId)
        {
            var technicianSpecialties = await technicianSpecialtyRepository.ListByTechnicianIdAsync(technicianId);
            var specialties = technicianSpecialties.Select(ut => ut.Specialty).ToList();
            return specialties;
        }

        public async Task<SpecialtyResponse> SaveAsync(Specialty specialty)
 		{
 			try
 			{
 				await _specialtyRepository.AddAsync(specialty);
 				await _unitOfWork.CompleteAsync();
 				return new SpecialtyResponse(specialty);
 			}
 			catch (Exception e)
 			{
 				return new SpecialtyResponse($"An error ocurred while saving the specialty: {e.Message}");
 			}
 		}
 		public async Task<SpecialtyResponse> UpdateAsync(int id, Specialty specialty)
 		{
 			var existingSpecialty = await _specialtyRepository.FindById(id);
 
 			if (existingSpecialty == null)
 				return new SpecialtyResponse("Specialty not found");

            existingSpecialty.Name = specialty.Name;

            try
            {
 				_specialtyRepository.Update(existingSpecialty);
 				await _unitOfWork.CompleteAsync();
 				return new SpecialtyResponse(existingSpecialty);
 			}
 			catch (Exception ex)
 			{
 				return new SpecialtyResponse($"An error ocurred while updating the specialty: {ex.Message}");
 			}
 		}
 	}
 }
