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
	public class ReportRepository : BaseRepository, IReportRepository
	{

		public ReportRepository(AppDbContext context) : base(context)
		{
		}

		public async Task AddAsync(Report report)
		{
			await _context.Reports.AddAsync(report);
		}

		public async Task<Report> FindById(int id)
		{
			return await _context.Reports.FindAsync(id);
		}

		public async Task<IEnumerable<Report>> ListAsync()
		{
			return await _context.Reports
				.Include(report => report.Customer)
			    .Include(report => report.Technician)
				.ToListAsync();
		}

		public void Remove(Report report)
		{
			_context.Reports.Remove(report);
		}

		public void Update(Report report)
		{
			_context.Reports.Update(report);
		}
	}
}
