using FluentAssertions;
using Moq;
using NUnit.Framework;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Services;
using SBA_BACKEND.User.AgreggatesModel;
using SBA_BACKEND.User.User.API.Services.Communications;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBA_BACKEND.Test
{
    class AddressServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoTechnicianFoundReturnsTechnicianNotFoundResponse()
        {
            // Arrange
            var mockAddressRepository = GetDefaultIAddressRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var addressId = 1;
            mockAddressRepository.Setup(r => r.FindById(addressId))
                .Returns(Task.FromResult<Address>(null));

            var service = new AddressService(mockAddressRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

            // Act
            AddressResponse result = await service.GetByIdAsync(addressId);
            var message = result.Message;

            // Assert
            message.Should().Be("Address not found");
        }

        private Mock<IAddressRepository> GetDefaultIAddressRepositoryInstance()
        {
            return new Mock<IAddressRepository>();
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
