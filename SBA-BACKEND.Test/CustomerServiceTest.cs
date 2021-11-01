﻿using NUnit.Framework;
using Moq;
using FluentAssertions;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SBA_BACKEND.Test
{
    public class CustomerServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCustomerFoundReturnsCustomerNotFoundResponse()
        {
            // Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var customerId = 1;
            Customer c = new()
            {
                UserId = 1,
                FirstName = "Jose"
            };
            mockCustomerRepository.Setup(r => r.FindById(customerId))
                .Returns(Task.FromResult<Customer>(c));

            var service = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

            // Act
            CustomerResponse result = await service.GetByIdAsync(customerId);
            var message = result.Message;

            // Assert
            message.Should().Be("");
        }

        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
    }
}
