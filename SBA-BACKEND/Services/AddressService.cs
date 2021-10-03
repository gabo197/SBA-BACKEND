using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using SBA_BACKEND.Domain.Models;
 using SBA_BACKEND.Domain.Services.Communications;
 using SBA_BACKEND.Domain.Services;
 using SBA_BACKEND.Domain.Persistence.Repositories;
 
 
 namespace SBA_BACKEND.Services
 {
 	public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IAddressRepository addressRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public async Task<AddressResponse> DeleteAsync(int id)
 		{
 			var existingAddress = await _addressRepository.FindById(id);
 
 			if (existingAddress == null)
 				return new AddressResponse("Address not found");
 
 			try
 			{
 				_addressRepository.Remove(existingAddress);
 				await _unitOfWork.CompleteAsync();
 
 				return new AddressResponse(existingAddress);
 			}
 			catch (Exception ex)
 			{
 				return new AddressResponse($"An error ocurred while deleting the address: {ex.Message}");
 			}
 		}
 
 		public async Task<AddressResponse> GetByIdAsync(int id)
 		{
 			var existingAddress = await _addressRepository.FindById(id);
 
 			if (existingAddress == null)
 				return new AddressResponse("Address not found");
 			return new AddressResponse(existingAddress);
 		}
 
 		public async Task<IEnumerable<Address>> ListAsync()
 		{
 			return await _addressRepository.ListAsync();
 		}
 
 		public async Task<AddressResponse> SaveAsync(int userId, Address address)
 		{
            var existingUser = await userRepository.FindById(userId);
            if (existingUser == null)
                return new AddressResponse("User not found");
            try
 			{
                address.UserId = userId;
 				await _addressRepository.AddAsync(address);
 				await _unitOfWork.CompleteAsync();
 				return new AddressResponse(address);
 			}
 			catch (Exception e)
 			{
 				return new AddressResponse($"An error ocurred while saving the address: {e.Message}");
 			}
 		}
 		public async Task<AddressResponse> UpdateAsync(int id, Address address)
 		{
 			var existingAddress = await _addressRepository.FindById(id);
 
 			if (existingAddress == null)
 				return new AddressResponse("Address not found");

            existingAddress.Region = address.Region;
            existingAddress.Province = address.Province;
            existingAddress.District = address.District;
            existingAddress.FullAddress = address.FullAddress;
 
 			try
 			{
 				_addressRepository.Update(existingAddress);
 				await _unitOfWork.CompleteAsync();
 				return new AddressResponse(existingAddress);
 			}
 			catch (Exception ex)
 			{
 				return new AddressResponse($"An error ocurred while updating the address: {ex.Message}");
 			}
 		}
 	}
 }
