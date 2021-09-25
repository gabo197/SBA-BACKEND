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
    public class TechnicianServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoTechnicianFoundReturnsTechnicianNotFoundResponse()
        {
            // Arrange
            var mockTechnicianRepository = GetDefaultITechnicianRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var technicianId = 1;
            mockTechnicianRepository.Setup(r => r.FindById(technicianId))
                .Returns(Task.FromResult<Technician>(null));

            var service = new TechnicianService(mockTechnicianRepository.Object, mockUnitOfWork.Object);

            // Act
            TechnicianResponse result = await service.GetByIdAsync(technicianId);
            var message = result.Message;

            // Assert
            message.Should().Be("Technician not found");
        }

        private Mock<ITechnicianRepository> GetDefaultITechnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
