using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Customer.Customer.API.Services.Communications;

namespace SBA_BACKEND.Customer.Customer.API.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Domain.AgreggatesModel.Customer>> ListAsync();
        Task<CustomerResponse> GetByIdAsync(int id);
        Task<CustomerResponse> SaveAsync(int userId, Domain.AgreggatesModel.Customer customer);
        Task<CustomerResponse> UpdateAsync(int id, Domain.AgreggatesModel.Customer customer);
        Task<CustomerResponse> DeleteAsync(int id);

    }
}
