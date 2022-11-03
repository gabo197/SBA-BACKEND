using NUnit.Framework;
using Moq;
using FluentAssertions;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Services.Communications;

namespace SBA_BACKEND.Test
{
    public class OpinionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoOpinionFoundReturnsOpinionNotFoundResponse()
        {
            // Arrange
            var mockOpinionRepository = GetDefaultIOpinionRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockTechnicianRepository = GetDefaultITechnicianRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var opinionId = 1;
            mockOpinionRepository.Setup(r => r.FindById(opinionId))
                .Returns(Task.FromResult<Opinion>(null));

            var service = new OpinionService(mockOpinionRepository.Object, mockUnitOfWork.Object, mockTechnicianRepository.Object, mockCustomerRepository.Object);

            // Act
            OpinionResponse result = await service.GetByIdAsync(opinionId);
            var message = result.Message;

            // Assert
            message.Should().Be("Opinion not found");
        }

        private Mock<IOpinionRepository> GetDefaultIOpinionRepositoryInstance()
        {
            return new Mock<IOpinionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<ITechnicianRepository> GetDefaultITechnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }

        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }
    }
}
