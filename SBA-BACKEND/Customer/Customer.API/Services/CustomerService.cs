using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.API.Services.Communications;

namespace SBA_BACKEND.Customer.Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository userRepository;
        private IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository object1, IUnitOfWork object2, IUserRepository userRepository)
        {
            _customerRepository = object1;
            _unitOfWork = object2;
            this.userRepository = userRepository;
        }

        public async Task<CustomerResponse> DeleteAsync(int id)
        {
            var existingCustomer = await _customerRepository.FindById(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            try
            {
                _customerRepository.Remove(existingCustomer);
                await _unitOfWork.CompleteAsync();

                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error ocurred while deleting the customer: {ex.Message}");
            }
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var existingCustomer = await _customerRepository.FindById(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");
            return new CustomerResponse(existingCustomer);
        }

        public async Task<IEnumerable<Customer.Domain.AgreggatesModel.Customer>> ListAsync()
        {
            return await _customerRepository.ListAsync();
        }

        public async Task<CustomerResponse> SaveAsync(int userId, Customer.Domain.AgreggatesModel.Customer customer)
        {
            var existingUser = await userRepository.FindById(userId);
            if (existingUser == null)
                return new CustomerResponse("User not found");
            try
            {
                customer.UserId = userId;
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.CompleteAsync();
                return new CustomerResponse(customer);
            }
            catch (Exception e)
            {
                return new CustomerResponse($"An error ocurred while saving the customer: {e.Message}");
            }
        }
        public async Task<CustomerResponse> UpdateAsync(int id, Customer.Domain.AgreggatesModel.Customer customer)
        {
            var existingCustomer = await _customerRepository.FindById(id);

            if (existingCustomer == null)
                return new CustomerResponse("Customer not found");

            existingCustomer.Description = customer.Description;
            existingCustomer.ImageUrl = customer.ImageUrl;
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            try
            {
                _customerRepository.Update(existingCustomer);
                await _unitOfWork.CompleteAsync();
                return new CustomerResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse($"An error ocurred while updating the customer: {ex.Message}");
            }
        }
    }
}
