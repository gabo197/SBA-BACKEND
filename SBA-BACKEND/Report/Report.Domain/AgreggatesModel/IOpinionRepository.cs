using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Report.Report.Domain.AgreggatesModel
{
    public interface IOpinionRepository
    {
        Task<IEnumerable<Opinion>> ListAsync();
        Task AddAsync(Opinion opinion);
        Task<Opinion> FindById(int id);
        void Update(Opinion opinion);
        void Remove(Opinion opinion);
    }
}
