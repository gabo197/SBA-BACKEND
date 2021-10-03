using SBA_BACKEND.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Persistence.Repositories
{	public interface IAddressRepository
	{
		Task<IEnumerable<Address>> ListAsync();
		Task AddAsync(Address address);
		Task<Address> FindById(int id);
		void Update(Address address);
		void Remove(Address address);
	}
}
