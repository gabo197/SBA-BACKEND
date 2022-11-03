using NUnit.Framework;
using Moq;
using FluentAssertions;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Persistence.Repositories;
using SBA_BACKEND.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Services.Communications;

namespace SBA_BACKEND.Test
{
    public class ReportServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoReportFoundReturnsReportNotFoundResponse()
        {
            // Arrange
            var mockReportRepository = GetDefaultIReportRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockTechnicianRepository = GetDefaultITechnicianRepositoryInstance();
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var reportId = 1;
            mockReportRepository.Setup(r => r.FindById(reportId))
                .Returns(Task.FromResult<Report>(null));

            var service = new ReportService(mockReportRepository.Object, mockUnitOfWork.Object, mockCustomerRepository.Object, mockTechnicianRepository.Object);

            // Act
            ReportResponse result = await service.GetByIdAsync(reportId);
            var message = result.Message;

            // Assert
            message.Should().Be("Report not found");
        }

        private Mock<IReportRepository> GetDefaultIReportRepositoryInstance()
        {
            return new Mock<IReportRepository>();
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
