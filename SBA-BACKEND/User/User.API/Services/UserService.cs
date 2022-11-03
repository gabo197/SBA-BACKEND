using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;
using SBA_BACKEND.User.User.API.Services.Communications;

namespace SBA_BACKEND.User.User.API.Services
{
    public class UserService : IUserService
    {
        private AppSettings appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IOptions<AppSettings> appSettings, IUserRepository object1, IUnitOfWork object2)
        {
            this.appSettings = appSettings.Value;
            _userRepository = object1;
            _unitOfWork = object2;
        }

        private string GenerateJwtToken(User.Domain.AgreggatesModel.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var users = await _userRepository.ListAsync();
            var user = users.SingleOrDefault(x => x.Email == request.Email
            && x.Password == request.Password);

            if (user == null) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User.Domain.AgreggatesModel.User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<List<string>> ListAllEmailsAsync()
        {
            return await _userRepository.ListAllEmailsAsync();
        }

        public async Task<UserResponse> SaveAsync(User.Domain.AgreggatesModel.User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error ocurred while saving the user: {e.Message}");
            }
        }
        public async Task<UserResponse> UpdateAsync(int id, User.Domain.AgreggatesModel.User user)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UserType = user.UserType;

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }
        }
    }
}
