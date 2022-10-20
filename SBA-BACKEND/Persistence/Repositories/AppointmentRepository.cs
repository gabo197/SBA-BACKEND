using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;
using System.Collections.Generic;
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
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> ListAsync()
        {
            return await _context.Appointments.ToListAsync();
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
