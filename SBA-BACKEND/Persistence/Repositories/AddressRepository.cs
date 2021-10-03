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
	public class AddressRepository : BaseRepository, IAddressRepository
	{

		public AddressRepository(AppDbContext context) : base(context)
		{
		}

		public async Task AddAsync(Address address)
		{
			await _context.Address.AddAsync(address);
		}

		public async Task<Address> FindById(int id)
		{
			return await _context.Address.FindAsync(id);
		}

		public async Task<IEnumerable<Address>> ListAsync()
		{
			return await _context.Address
				.ToListAsync();
		}

		public void Remove(Address address)
		{
			_context.Address.Remove(address);
		}

		public void Update(Address address)
		{
			_context.Address.Update(address);
		}
	}
}
