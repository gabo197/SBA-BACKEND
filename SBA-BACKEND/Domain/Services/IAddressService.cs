using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Domain.Services
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
