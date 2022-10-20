using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services.Communications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> ListAsync();
        Task<AppointmentResponse> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByCustomerIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByTechnicianIdAsync(int id);
        Task<AppointmentResponse> SaveAsync(Appointment appointment);
        Task<AppointmentResponse> UpdateAsync(int id, Appointment appointment);
        Task<AppointmentResponse> DeleteAsync(int id);
    }
}
