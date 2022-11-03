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
    public class SpecialtyRepository : BaseRepository, ISpecialtyRepository
    {

        public SpecialtyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Specialty specialty)
        {
            await _context.Specialties.AddAsync(specialty);
        }

        public async Task<Specialty> FindById(int id)
        {
            return await _context.Specialties.FindAsync(id);
        }

        public async Task<IEnumerable<Specialty>> ListAsync()
        {
            return await _context.Specialties
                .ToListAsync();
        }

        public void Remove(Specialty specialty)
        {
            _context.Specialties.Remove(specialty);
        }

        public void Update(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
        }
    }
}
