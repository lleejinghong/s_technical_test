using AutoMapper;
using FluentAssertions;
using LjhBackendApi.Application.Contracts;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Register;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Registration;
using LjhBackendApi.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace LjhBackendApi.Application.UnitTests.Features.ApplicationUsers.Commands
{
    public class RegistrationCommandTests
    {
        private Mock<IApplicationUsersRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        private RegistrationCommand _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IApplicationUsersRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RegistrationCommand(_mockRepo.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_WhenUserAlreadyExists_ReturnsFailureResult()
        {
            // Arrange
            var dto = new RegistrationDto { Email = "existing@example.com" };
            _mockRepo.Setup(r => r.GetUser(dto.Email))
                .ReturnsAsync(new ApplicationUser());

            var request = new RegistrationRequest { RegistrationDto = dto };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().Contain("User with this email already exists.");
        }

        [Test]
        public async Task Handle_WhenUserIsNew_ReturnsSuccessResult()
        {
            // Arrange
            var dto = new RegistrationDto
            {
                Email = "newuser@example.com",
                FirstName = "New",
                LastName = "User",
                Password = "Secure123"
            };

            var user = new ApplicationUser();

            _mockRepo.Setup(r => r.GetUser(dto.Email)).ReturnsAsync((ApplicationUser)null!);
            _mockMapper.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _mockRepo.Setup(r => r.CreateUser(user)).ReturnsAsync(Guid.NewGuid());

            var request = new RegistrationRequest { RegistrationDto = dto };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenCreateUserFails_ReturnsFailure()
        {
            // Arrange
            var dto = new RegistrationDto
            {
                Email = "fail@example.com",
                FirstName = "Fail",
                LastName = "Case",
                Password = "Test123"
            };

            var user = new ApplicationUser();

            _mockRepo.Setup(r => r.GetUser(dto.Email)).ReturnsAsync((ApplicationUser)null!);
            _mockMapper.Setup(m => m.Map<ApplicationUser>(dto)).Returns(user);
            _mockRepo.Setup(r => r.CreateUser(user)).ReturnsAsync(Guid.Empty);

            var request = new RegistrationRequest { RegistrationDto = dto };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().Contain("Failed to register user.");
        }
    }
}
