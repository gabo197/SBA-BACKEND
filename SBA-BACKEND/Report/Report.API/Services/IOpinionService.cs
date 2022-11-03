using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Report.Report.API.Services.Communications;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;

namespace SBA_BACKEND.Report.Report.API.Services
{
    public interface IOpinionService
    {
        Task<IEnumerable<Opinion>> ListAsync();
        Task<OpinionResponse> GetByIdAsync(int id);
        Task<OpinionResponse> SaveAsync(int customerId, int technicianId, Opinion opinion);
        Task<OpinionResponse> UpdateAsync(int id, Opinion opinion);
        Task<OpinionResponse> DeleteAsync(int id);

    }
}
