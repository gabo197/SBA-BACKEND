using NUnit.Framework;
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
    public class DistrictServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoDistrictFoundReturnsDistrictNotFoundResponse()
        {
            // Arrange
            var mockDistrictRepository = GetDefaultIDistrictRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var districtId = 1;
            mockDistrictRepository.Setup(r => r.FindById(districtId))
                .Returns(Task.FromResult<District>(null));

            var service = new AddressService(mockDistrictRepository.Object, mockUnitOfWork.Object);

            // Act
            AddressResponse result = await service.GetByIdAsync(districtId);
            var message = result.Message;

            // Assert
            message.Should().Be("District not found");
        }

        private Mock<IAddressRepository> GetDefaultIDistrictRepositoryInstance()
        {
            return new Mock<IAddressRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
