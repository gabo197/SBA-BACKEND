using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.User.User.API.Services.Communications;

namespace SBA_BACKEND.User.User.API.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        Task<IEnumerable<User.Domain.AgreggatesModel.User>> ListAsync();
        Task<List<string>> ListAllEmailsAsync();
        Task<UserResponse> GetByIdAsync(int id);
        Task<UserResponse> SaveAsync(User.Domain.AgreggatesModel.User user);
        Task<UserResponse> UpdateAsync(int id, User.Domain.AgreggatesModel.User user);
        Task<UserResponse> DeleteAsync(int id);

    }
}
