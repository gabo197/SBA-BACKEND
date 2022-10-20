using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Domain.Services.Communications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Services
{
    public class AppointmentService : IAppointmentService
    {
        public Task<AppointmentResponse> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppointmentResponse> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> ListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<AppointmentResponse> SaveAsync(Appointment appointment)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppointmentResponse> UpdateAsync(int id, Appointment appointment)
        {
            throw new System.NotImplementedException();
        }
    }
}
