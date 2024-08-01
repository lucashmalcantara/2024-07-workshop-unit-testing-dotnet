using AutoFixture;
using AutoFixture.AutoMoq;
using Examples.Notification.Api.Application.Commands.SendEmail;
using Examples.Notification.Api.Core.Services;
using Examples.Notification.Api.Domain.Models;
using Examples.Notification.Api.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;

namespace Examples.Notification.UnitTests.Application.Commands.SendEmail
{
    public class SendEmailCommandHandlerTests
    {
        [Fact]
        public async Task GIVEN_invalid_values_WHEN_sending_email_message_THEN_return_error()
        {
            // Arrange
            var customization = new AutoMoqCustomization() { ConfigureMembers = true };
            var fixture = new Fixture().Customize(customization);

            var invalidCommand = new SendEmailCommand(From: "invalid", To: "to@host.com", Subject: "A subject", Message: "A message");

            var blockedTerms = new[] { "abc", "xyz" };

            // The Freeze feature in AutoFixture allows you to freeze an instance of a specific type so
            // that the same instance is used consistently throughout a test.
            //
            // In this case, we need this to take advantage of the stub and the Verify method that will be used next.
            // If we don't use Freeze, AutoFixture will create another instance of Mock<IBlockedTermsRepository> that won't contain the assigned configurations.
            var blockedTermsRepositoryMock = fixture.Freeze<Mock<IBlockedTermsRepository>>();
            var emailSenderServiceMock = fixture.Freeze<Mock<IEmailSenderService>>();

            blockedTermsRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(blockedTerms);

            var sendEmailCommandHandler = fixture.Create<SendEmailCommandHandler>();

            // Act
            var error = await sendEmailCommandHandler.HandleAsync(invalidCommand);

            // Assert
            error.Should().NotBeNull();
            error?.Type.Should().Be("invalid-values");
            error?.Title.Should().Be("Invalid values");
            error?.Status.Should().Be(StatusCodes.Status400BadRequest);
            error?.Detail.Should().Be("The message contains invalid values.");
            error?.Extensions.Should().HaveCount(1).And.ContainKey("validationErrors");

            blockedTermsRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

            emailSenderServiceMock.Verify(x => x.SendAsync(It.IsAny<EmailMessage>()), Times.Never);
        }

        [Fact]
        public async Task GIVEN_valid_values_WHEN_sending_email_message_THEN_do_not_return_error()
        {
            // Arrange
            var customization = new AutoMoqCustomization() { ConfigureMembers = true };
            var fixture = new Fixture().Customize(customization);

            var invalidCommand = new SendEmailCommand(From: "from@host.com", To: "to@host.com", Subject: "A subject", Message: "A message");

            var blockedTerms = new[] { "abc", "xyz" };

            var blockedTermsRepositoryMock = fixture.Freeze<Mock<IBlockedTermsRepository>>();
            var emailSenderServiceMock = fixture.Freeze<Mock<IEmailSenderService>>();

            blockedTermsRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(blockedTerms);

            var sendEmailCommandHandler = fixture.Create<SendEmailCommandHandler>();

            // Act
            var error = await sendEmailCommandHandler.HandleAsync(invalidCommand);

            // Assert
            error.Should().BeNull();

            blockedTermsRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

            emailSenderServiceMock.Verify(x => x.SendAsync(It.IsAny<EmailMessage>()), Times.Once);
        }
    }
}
