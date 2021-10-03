using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;

namespace SBA_BACKEND.Persistence.Repositories
{
    public class TechnicianSpecialtyRepository : BaseRepository, ITechnicianSpecialtyRepository
    {
        public TechnicianSpecialtyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TechnicianSpecialty technicianSpecialty)
        {
            await _context.TechnicianSpecialties.AddAsync(technicianSpecialty);
        }

        public async Task AssignTechnicianSpecialty(int technicianId, int specialtyId)
        {
            TechnicianSpecialty technicianSpecialty = await FindByTechnicianIdAndSpecialtyId(technicianId, specialtyId);
            if (technicianSpecialty == null)
            {
                technicianSpecialty = new TechnicianSpecialty { TechnicianId = technicianId, SpecialtyId = specialtyId };
                await AddAsync(technicianSpecialty);
            }
        }

        public async Task<TechnicianSpecialty> FindByTechnicianIdAndSpecialtyId(int technicianId, int specialtyId)
        {
            return await _context.TechnicianSpecialties.FindAsync(technicianId, specialtyId);
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListAsync()
        {
            return await _context.TechnicianSpecialties
                .Include(ts => ts.Technician)
                .Include(ts => ts.Specialty)
                .ToListAsync();
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListBySpecialtyIdAsync(int specialtyId)
        {
            return await _context.TechnicianSpecialties
                .Where(ts => ts.SpecialtyId == specialtyId)
                .Include(ts => ts.Technician)
                .Include(ts => ts.Specialty)
                .ToListAsync();
        }

        public async Task<IEnumerable<TechnicianSpecialty>> ListByTechnicianIdAsync(int technicianId)
        {
            return await _context.TechnicianSpecialties
                .Where(ts => ts.TechnicianId == technicianId)
                .Include(ts => ts.Technician)
                .Include(ts => ts.Specialty)
                .ToListAsync();
        }

        public void Remove(TechnicianSpecialty technicianSpecialty)
        {
            _context.TechnicianSpecialties.Remove(technicianSpecialty);
        }

        public async void UnassignTechnicianSpecialty(int technicianId, int specialtyId)
        {
            TechnicianSpecialty technicianSpecialty = await _context.TechnicianSpecialties.FindAsync(technicianId, specialtyId);
            if (technicianSpecialty != null)
                Remove(technicianSpecialty);
        }
    }
}
