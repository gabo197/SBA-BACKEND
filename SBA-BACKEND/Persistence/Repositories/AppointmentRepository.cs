using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Persistence.Repositories
{
    public class AppointmentRepository: BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public async Task<Appointment> FindById(int id)
        {
            return await _context.Appointments.Include(x => x.PaymentMethod).FirstOrDefaultAsync(x => x.AppointmentId == id);
        }

        public async Task<IEnumerable<Appointment>> ListAsync()
        {
            return await _context.Appointments.Include(x => x.PaymentMethod).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> ListByCustomerIdAsync(int id)
        {
            return await _context.Appointments.Include(x => x.PaymentMethod).Include(x => x.Technician).Where(x => x.CustomerId == id).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> ListByTechnicianIdAsync(int id)
        {
            return await _context.Appointments.Include(x => x.PaymentMethod).Include(x => x.Customer).Where(x => x.TechnicianId == id).ToListAsync();
        }

        public void Remove(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }
    }
}
