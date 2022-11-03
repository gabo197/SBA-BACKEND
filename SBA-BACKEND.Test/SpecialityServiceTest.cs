using NUnit.Framework;
using Moq;
using FluentAssertions;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SBA_BACKEND.Technician.Technician.API.Services.Communications;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;

namespace SBA_BACKEND.Test
{
    public class SpecialtyServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoSpecialtyFoundReturnsSpecialtyNotFoundResponse()
        {
            // Arrange
            var mockSpecialtyRepository = GetDefaultISpecialtyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockTechnicianSpecialtyRepository = GetDefaultITechnicianSpecialtyRepositoryInstance();
            var specialtyId = 1;
            mockSpecialtyRepository.Setup(r => r.FindById(specialtyId))
                .Returns(Task.FromResult<Specialty>(null));

            var service = new SpecialtyService(mockSpecialtyRepository.Object, mockUnitOfWork.Object, mockTechnicianSpecialtyRepository.Object);

            // Act
            SpecialtyResponse result = await service.GetByIdAsync(specialtyId);
            var message = result.Message;

            // Assert
            message.Should().Be("Specialty not found");
        }

        private Mock<ISpecialtyRepository> GetDefaultISpecialtyRepositoryInstance()
        {
            return new Mock<ISpecialtyRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<ITechnicianSpecialtyRepository> GetDefaultITechnicianSpecialtyRepositoryInstance()
        {
            return new Mock<ITechnicianSpecialtyRepository>();
        }
    }
}
