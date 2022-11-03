using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.User.User.API.Services.Communications;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;

namespace SBA_BACKEND.User.User.API.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> ListAsync();
        Task<AddressResponse> GetByIdAsync(int id);
        Task<AddressResponse> SaveAsync(int userId, Address address);
        Task<AddressResponse> UpdateAsync(int id, Address address);
        Task<AddressResponse> DeleteAsync(int id);

    }
}
