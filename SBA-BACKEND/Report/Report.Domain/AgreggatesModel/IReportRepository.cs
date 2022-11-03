using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Report.Report.Domain.AgreggatesModel
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> ListAsync();
        Task AddAsync(Report report);
        Task<Report> FindById(int id);
        void Update(Report report);
        void Remove(Report report);
    }
}
