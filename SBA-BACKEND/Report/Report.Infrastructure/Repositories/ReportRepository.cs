using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Persistence.Repositories;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;

namespace SBA_BACKEND.Report.Report.Infrastructure.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {

        public ReportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Report.Domain.AgreggatesModel.Report report)
        {
            await _context.Reports.AddAsync(report);
        }

        public async Task<Report.Domain.AgreggatesModel.Report> FindById(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<IEnumerable<Report.Domain.AgreggatesModel.Report>> ListAsync()
        {
            return await _context.Reports
                .Include(report => report.Customer)
                .Include(report => report.Technician)
                .ToListAsync();
        }

        public void Remove(Report.Domain.AgreggatesModel.Report report)
        {
            _context.Reports.Remove(report);
        }

        public void Update(Report.Domain.AgreggatesModel.Report report)
        {
            _context.Reports.Update(report);
        }
    }
}
