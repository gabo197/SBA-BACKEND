using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Persistence.Repositories;

namespace SBA_BACKEND.Customer.Customer.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {

        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Customer.Domain.AgreggatesModel.Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer.Domain.AgreggatesModel.Customer> FindById(int id)
        {
            return await _context.Customers.Include(x => x.User).Include(x => x.User.Address).FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<IEnumerable<Customer.Domain.AgreggatesModel.Customer>> ListAsync()
        {
            return await _context.Customers.Include(x => x.User).Include(x => x.User.Address).ToListAsync();
        }

        public void Remove(Customer.Domain.AgreggatesModel.Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public void Update(Customer.Domain.AgreggatesModel.Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
