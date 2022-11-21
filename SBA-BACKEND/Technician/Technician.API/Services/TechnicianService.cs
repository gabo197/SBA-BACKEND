using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;
using SBA_BACKEND.Technician.Technician.API.Services.Communications;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;

namespace SBA_BACKEND.Technician.Technician.API.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IUserRepository userRepository;
        private IUnitOfWork _unitOfWork;
        public TechnicianService(ITechnicianRepository object1, IUnitOfWork object2, IUserRepository userRepository)
        {
            _technicianRepository = object1;
            _unitOfWork = object2;
            this.userRepository = userRepository;
        }

        public async Task<TechnicianResponse> DeleteAsync(int id)
        {
            var existingTechnician = await _technicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");

            try
            {
                _technicianRepository.Remove(existingTechnician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception ex)
            {
                return new TechnicianResponse($"An error ocurred while deleting the technician: {ex.Message}");
            }
        }

        public async Task<TechnicianResponse> GetByIdAsync(int id)
        {
            var existingTechnician = await _technicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");
            return new TechnicianResponse(existingTechnician);
        }

        public async Task<IEnumerable<Technician.Domain.AgreggatesModel.Technician>> ListAsync()
        {
            return await _technicianRepository.ListAsync();
        }

        public async Task<IEnumerable<Domain.AgreggatesModel.Technician>> ListTechniciansBySpecialtyId(int specialtyId)
        {
            return await _technicianRepository.ListBySpecialty(specialtyId);
        }

        public async Task<TechnicianResponse> SaveAsync(int userId, Technician.Domain.AgreggatesModel.Technician technician)
        {
            var existingUser = await userRepository.FindById(userId);
            if (existingUser == null)
                return new TechnicianResponse("User not found");
            try
            {
                technician.UserId = userId;
                await _technicianRepository.AddAsync(technician);
                await _unitOfWork.CompleteAsync();
                return new TechnicianResponse(technician);
            }
            catch (Exception e)
            {
                return new TechnicianResponse($"An error ocurred while saving the technician: {e.Message}");
            }
        }
        public async Task<TechnicianResponse> UpdateAsync(int id, Technician.Domain.AgreggatesModel.Technician technician)
        {
            var existingTechnician = await _technicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");

            existingTechnician.Description = technician.Description;
            existingTechnician.ImageUrl = technician.ImageUrl;
            existingTechnician.FirstName = technician.FirstName;
            existingTechnician.LastName = technician.LastName;
            existingTechnician.PhoneNumber = technician.PhoneNumber;

            try
            {
                _technicianRepository.Update(existingTechnician);
                await _unitOfWork.CompleteAsync();
                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception ex)
            {
                return new TechnicianResponse($"An error ocurred while updating the technician: {ex.Message}");
            }
        }
    }
}
