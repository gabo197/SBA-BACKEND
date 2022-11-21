using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Persistence.Repositories;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;

namespace SBA_BACKEND.Technician.Technician.Infrastructure.Repositories
{
    public class TechnicianRepository : BaseRepository, ITechnicianRepository
    {

        public TechnicianRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Technician.Domain.AgreggatesModel.Technician technician)
        {
            await _context.Technicians.AddAsync(technician);
        }

        public async Task<Technician.Domain.AgreggatesModel.Technician> FindById(int id)
        {
            return await _context.Technicians.Include(x => x.User).Include(x => x.User.Address).FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<IEnumerable<Technician.Domain.AgreggatesModel.Technician>> ListAsync()
        {
            return await _context.Technicians
                .ToListAsync();
        }

        public void Remove(Technician.Domain.AgreggatesModel.Technician technician)
        {
            _context.Technicians.Remove(technician);
        }

        public void Update(Technician.Domain.AgreggatesModel.Technician technician)
        {
            _context.Technicians.Update(technician);
        }
    }
}
