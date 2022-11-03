using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Services.Communications;

namespace SBA_BACKEND.Report.Report.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ITechnicianRepository technicianRepository;
        private IUnitOfWork _unitOfWork;
        public ReportService(IReportRepository object1, IUnitOfWork object2, ICustomerRepository customerRepository, ITechnicianRepository technicianRepository)
        {
            _reportRepository = object1;
            _unitOfWork = object2;
            this.customerRepository = customerRepository;
            this.technicianRepository = technicianRepository;
        }

        public async Task<ReportResponse> DeleteAsync(int id)
        {
            var existingReport = await _reportRepository.FindById(id);

            if (existingReport == null)
                return new ReportResponse("Report not found");

            try
            {
                _reportRepository.Remove(existingReport);
                await _unitOfWork.CompleteAsync();

                return new ReportResponse(existingReport);
            }
            catch (Exception ex)
            {
                return new ReportResponse($"An error ocurred while deleting the report: {ex.Message}");
            }
        }

        public async Task<ReportResponse> GetByIdAsync(int id)
        {
            var existingReport = await _reportRepository.FindById(id);

            if (existingReport == null)
                return new ReportResponse("Report not found");
            return new ReportResponse(existingReport);
        }

        public async Task<IEnumerable<Report.Domain.AgreggatesModel.Report>> ListAsync()
        {
            return await _reportRepository.ListAsync();
        }

        public async Task<ReportResponse> SaveAsync(int customerId, int technicianId, Report.Domain.AgreggatesModel.Report report)
        {
            var existingCustomer = await customerRepository.FindById(customerId);
            if (existingCustomer == null)
                return new ReportResponse("Customer not found");
            var existingTechnician = await technicianRepository.FindById(technicianId);
            if (existingTechnician == null)
                return new ReportResponse("Technician not found");
            try
            {
                report.CustomerId = customerId;
                report.TechnicianId = technicianId;
                await _reportRepository.AddAsync(report);
                await _unitOfWork.CompleteAsync();
                return new ReportResponse(report);
            }
            catch (Exception e)
            {
                return new ReportResponse($"An error ocurred while saving the report: {e.Message}");
            }
        }
        public async Task<ReportResponse> UpdateAsync(int id, Report.Domain.AgreggatesModel.Report report)
        {
            var existingReport = await _reportRepository.FindById(id);

            if (existingReport == null)
                return new ReportResponse("Report not found");

            existingReport.Description = report.Description;

            try
            {
                _reportRepository.Update(existingReport);
                await _unitOfWork.CompleteAsync();
                return new ReportResponse(existingReport);
            }
            catch (Exception ex)
            {
                return new ReportResponse($"An error ocurred while updating the report: {ex.Message}");
            }
        }
    }
}
