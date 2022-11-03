using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Persistence.Repositories;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;

namespace SBA_BACKEND.Report.Report.Infrastructure.Repositories
{
    public class OpinionRepository : BaseRepository, IOpinionRepository
    {

        public OpinionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Opinion opinion)
        {
            await _context.Opinions.AddAsync(opinion);
        }

        public async Task<Opinion> FindById(int id)
        {
            return await _context.Opinions.FindAsync(id);
        }

        public async Task<IEnumerable<Opinion>> ListAsync()
        {
            return await _context.Opinions
                .Include(opinion => opinion.Customer)
                .Include(opinion => opinion.Technician)
                .ToListAsync();
        }

        public void Remove(Opinion opinion)
        {
            _context.Opinions.Remove(opinion);
        }

        public void Update(Opinion opinion)
        {
            _context.Opinions.Update(opinion);
        }
    }
}
