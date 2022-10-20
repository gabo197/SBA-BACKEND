using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<AppointmentResponse> DeleteAsync(int id)
        {
            var existingAppointment = await _appointmentRepository.FindById(id);

            if (existingAppointment == null)
                return new AppointmentResponse("Address not found");

            try
            {
                _appointmentRepository.Remove(existingAppointment);
                await _unitOfWork.CompleteAsync();

                return new AppointmentResponse(existingAppointment);
            }
            catch (Exception ex)
            {
                return new AppointmentResponse($"An error ocurred while deleting the appointment: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int id)
        {
            return await _appointmentRepository.ListByCustomerIdAsync(id);
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var existingAppointment = await _appointmentRepository.FindById(id);

            if (existingAppointment == null)
                return new AppointmentResponse("Appointment not found");
            return new AppointmentResponse(existingAppointment);
        }

        public async Task<IEnumerable<Appointment>> GetByTechnicianIdAsync(int id)
        {
            return await _appointmentRepository.ListByTechnicianIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> ListAsync()
        {
            return await _appointmentRepository.ListAsync();
        }

        public async Task<AppointmentResponse> SaveAsync(Appointment appointment)
        {
            try
            {
                await _appointmentRepository.AddAsync(appointment);
                await _unitOfWork.CompleteAsync();
                return new AppointmentResponse(appointment);
            }
            catch (Exception e)
            {
                return new AppointmentResponse($"An error ocurred while saving the appointment: {e.Message}");
            }
        }

        public async Task<AppointmentResponse> UpdateAsync(int id, Appointment appointment)
        {
            var existingAppointment = await _appointmentRepository.FindById(id);

            if (existingAppointment == null)
                return new AppointmentResponse("Address not found");

            existingAppointment.Status = appointment.Status;
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.Description = appointment.Description;
            existingAppointment.PaymentMethodId = appointment.PaymentMethodId;
            existingAppointment.Valorization = appointment.Valorization;

            try
            {
                _appointmentRepository.Update(existingAppointment);
                await _unitOfWork.CompleteAsync();
                return new AppointmentResponse(existingAppointment);
            }
            catch (Exception ex)
            {
                return new AppointmentResponse($"An error ocurred while updating the appointment: {ex.Message}");
            }
        }
    }
}
