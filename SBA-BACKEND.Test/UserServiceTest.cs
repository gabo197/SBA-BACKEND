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
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoUserFoundReturnsUserNotFoundResponse()
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));

            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;

            // Assert
            message.Should().Be("User not found");
        }

        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
