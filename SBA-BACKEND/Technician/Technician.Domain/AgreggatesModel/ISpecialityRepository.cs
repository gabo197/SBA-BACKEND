using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> ListAsync();
        Task AddAsync(Specialty specialty);
        Task<Specialty> FindById(int id);
        void Update(Specialty specialty);
        void Remove(Specialty specialty);
    }
}
