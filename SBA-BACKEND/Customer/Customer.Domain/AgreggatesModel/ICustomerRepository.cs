using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync();
        Task AddAsync(Customer customer);
        Task<Customer> FindById(int id);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
