using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBA_BACKEND.Domain.Persistence.Contexts;
using SBA_BACKEND.Persistence.Repositories;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;

namespace SBA_BACKEND.User.User.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User.Domain.AgreggatesModel.User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User.Domain.AgreggatesModel.User> FindById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User.Domain.AgreggatesModel.User>> ListAsync()
        {
            return await _context.Users
                .Include(user => user.Address)
                .ToListAsync();
        }

        public async Task<List<string>> ListAllEmailsAsync()
        {
            return await _context.Users.Select(x => x.Email).ToListAsync();
        }

        public void Remove(User.Domain.AgreggatesModel.User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User.Domain.AgreggatesModel.User user)
        {
            _context.Users.Update(user);
        }
    }
}
