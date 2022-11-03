using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.User.User.Domain.AgreggatesModel
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> ListAsync();
        Task AddAsync(Address address);
        Task<Address> FindById(int id);
        void Update(Address address);
        void Remove(Address address);
    }
}
