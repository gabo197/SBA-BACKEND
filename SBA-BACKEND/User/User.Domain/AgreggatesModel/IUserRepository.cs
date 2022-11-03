using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.User.User.Domain.AgreggatesModel
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<List<string>> ListAllEmailsAsync();
        Task AddAsync(User user);
        Task<User> FindById(int id);
        void Update(User user);
        void Remove(User user);
    }
}
