using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel
{
    public interface ITechnicianRepository
    {
        Task<IEnumerable<Technician>> ListAsync();
        Task AddAsync(Technician technician);
        Task<Technician> FindById(int id);
        void Update(Technician technician);
        void Remove(Technician technician);
    }
}
