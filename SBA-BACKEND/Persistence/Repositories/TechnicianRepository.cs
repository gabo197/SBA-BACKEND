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
	public class TechnicianRepository : BaseRepository, ITechnicianRepository
	{

		public TechnicianRepository(AppDbContext context) : base(context)
		{
		}

		public async Task AddAsync(Technician technician)
		{
			await _context.Technicians.AddAsync(technician);
		}

		public async Task<Technician> FindById(int id)
		{
			return await _context.Technicians.FindAsync(id);
		}

		public async Task<IEnumerable<Technician>> ListAsync()
		{
			return await _context.Technicians
				.ToListAsync();
		}

		public void Remove(Technician technician)
		{
			_context.Technicians.Remove(technician);
		}

		public void Update(Technician technician)
		{
			_context.Technicians.Update(technician);
		}
	}
}
