using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Domain.Services
{
	public interface ICustomerService
	{
		Task<IEnumerable<Customer>> ListAsync();
		Task<CustomerResponse> GetByIdAsync(int id);
		Task<CustomerResponse> SaveAsync(int userId, Customer customer);
		Task<CustomerResponse> UpdateAsync(int id, Customer customer);
		Task<CustomerResponse> DeleteAsync(int id);

	}
}
