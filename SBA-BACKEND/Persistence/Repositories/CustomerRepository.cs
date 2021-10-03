using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Domain.Persistence.Repositories;

namespace SBA_BACKEND.Persistence.Repositories
{
	public class CustomerRepository : BaseRepository, ICustomerRepository
	{

		public CustomerRepository(AppDbContext context) : base(context)
		{
		}

		public async Task AddAsync(Customer customer)
		{
			await _context.Customers.AddAsync(customer);
		}

		public async Task<Customer> FindById(int id)
		{
			return await _context.Customers.FindAsync(id);
		}

		public async Task<IEnumerable<Customer>> ListAsync()
		{
			return await _context.Customers.ToListAsync();
		}

		public void Remove(Customer customer)
		{
			_context.Customers.Remove(customer);
		}

		public void Update(Customer customer)
		{
			_context.Customers.Update(customer);
		}
	}
}
