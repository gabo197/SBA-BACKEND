using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Report.Report.API.Services.Communications;

namespace SBA_BACKEND.Report.Report.API.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Report.Domain.AgreggatesModel.Report>> ListAsync();
        Task<ReportResponse> GetByIdAsync(int id);
        Task<ReportResponse> SaveAsync(int customerId, int technicianId, Report.Domain.AgreggatesModel.Report report);
        Task<ReportResponse> UpdateAsync(int id, Report.Domain.AgreggatesModel.Report report);
        Task<ReportResponse> DeleteAsync(int id);

    }
}
