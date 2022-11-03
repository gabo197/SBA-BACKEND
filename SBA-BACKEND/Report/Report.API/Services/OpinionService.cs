using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Services.Communications;

namespace SBA_BACKEND.Report.Report.API.Services
{
    public class OpinionService : IOpinionService
    {
        private readonly IOpinionRepository _opinionRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ITechnicianRepository technicianRepository;
        private IUnitOfWork _unitOfWork;
        public OpinionService(IOpinionRepository object1, IUnitOfWork object2, ITechnicianRepository technicianRepository, ICustomerRepository customerRepository)
        {
            _opinionRepository = object1;
            _unitOfWork = object2;
            this.technicianRepository = technicianRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<OpinionResponse> DeleteAsync(int id)
        {
            var existingOpinion = await _opinionRepository.FindById(id);

            if (existingOpinion == null)
                return new OpinionResponse("Opinion not found");

            try
            {
                _opinionRepository.Remove(existingOpinion);
                await _unitOfWork.CompleteAsync();

                return new OpinionResponse(existingOpinion);
            }
            catch (Exception ex)
            {
                return new OpinionResponse($"An error ocurred while deleting the opinion: {ex.Message}");
            }
        }

        public async Task<OpinionResponse> GetByIdAsync(int id)
        {
            var existingOpinion = await _opinionRepository.FindById(id);

            if (existingOpinion == null)
                return new OpinionResponse("Opinion not found");
            return new OpinionResponse(existingOpinion);
        }

        public async Task<IEnumerable<Opinion>> ListAsync()
        {
            return await _opinionRepository.ListAsync();
        }

        public async Task<OpinionResponse> SaveAsync(int customerId, int technicianId, Opinion opinion)
        {
            var existingCustomer = await customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new OpinionResponse("Customer not found");
            var existingTechnician = await technicianRepository.FindById(technicianId);
            if (existingTechnician == null)
                return new OpinionResponse("Technician not found");
            try
            {
                opinion.CustomerId = customerId;
                opinion.TechnicianId = technicianId;
                await _opinionRepository.AddAsync(opinion);
                await _unitOfWork.CompleteAsync();
                return new OpinionResponse(opinion);
            }
            catch (Exception e)
            {
                return new OpinionResponse($"An error ocurred while saving the opinion: {e.Message}");
            }
        }
        public async Task<OpinionResponse> UpdateAsync(int id, Opinion opinion)
        {
            var existingOpinion = await _opinionRepository.FindById(id);

            if (existingOpinion == null)
                return new OpinionResponse("Opinion not found");

            existingOpinion.Description = opinion.Description;
            existingOpinion.Stars = opinion.Stars;

            try
            {
                _opinionRepository.Update(existingOpinion);
                await _unitOfWork.CompleteAsync();
                return new OpinionResponse(existingOpinion);
            }
            catch (Exception ex)
            {
                return new OpinionResponse($"An error ocurred while updating the opinion: {ex.Message}");
            }
        }
    }
}
