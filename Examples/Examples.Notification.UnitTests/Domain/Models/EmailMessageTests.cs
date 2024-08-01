using Examples.Notification.Api.Domain.Models;
using FluentAssertions;

namespace Examples.Notification.UnitTests.Domain.Models
{
    public class EmailMessageTests
    {
        [Theory]
        [InlineData("no_domain")]
        [InlineData("no_domain@")]
        [InlineData("incomplete_domain@domain")]
        public void GIVEN_invalid_From_WHEN_validating_THEN_return_error(string invalidFrom)
        {
            // Arrange
            var emailMessage = new EmailMessage(invalidFrom,
                to: "valid@email.com",
                subject: "A subject",
                message: "A message",
                blockedTerms: []);

            // Act
            var errors = emailMessage.Validate();

            // Assert
            errors.Should()
                .HaveCount(1)
                .And.Contain("Invalid 'from' e-mail.");
        }

        [Theory]
        [InlineData("no_domain")]
        [InlineData("no_domain@")]
        [InlineData("incomplete_domain@domain")]
        public void GIVEN_invalid_To_WHEN_validating_THEN_return_error(string invalidTo)
        {
            // Arrange
            var emailMessage = new EmailMessage(from: "valid@email.com",
                invalidTo,
                subject: "A subject",
                message: "A message",
                blockedTerms: []);

            // Act
            var errors = emailMessage.Validate();

            // Assert
            errors.Should()
                .HaveCount(1)
                .And.Contain("Invalid 'to' e-mail.");
        }

        [Fact]
        public void GIVEN_subject_has_less_than_three_characters_WHEN_validating_THEN_return_error()
        {
            // Arrange
            var invalidSubject = "ab";

            var emailMessage = new EmailMessage(from: "valid@email.com",
                to: "valid@email.com",
                invalidSubject,
                message: "A message",
                blockedTerms: []);

            // Act
            var errors = emailMessage.Validate();

            // Assert
            errors.Should()
                .HaveCount(1)
                .And.Contain("The subject must have three or more characters.");
        }

        [Theory]
        [InlineData("A message with invalid abc term.")]
        [InlineData("A message with invalid ABC term.")]
        [InlineData("A message with invalid xyz term.")]
        [InlineData("A message with invalid XYZ term.")]
        public void GIVEN_message_has_blocked_terms_WHEN_validating_THEN_return_error(string invalidMessage)
        {
            // Arrange
            var emailMessage = new EmailMessage(from: "valid@email.com",
                to: "valid@email.com",
                subject: "A subject",
                invalidMessage,
                blockedTerms: ["abc", "xyz"]);

            // Act
            var errors = emailMessage.Validate();

            // Assert
            errors.Should()
                .HaveCount(1)
                .And.Contain("The content of the message does not comply with our policies.");
        }

        [Fact]
        public void GIVEN_all_valid_values_WHEN_validating_THEN_return_empty()
        {
            // Arrange
            var emailMessage = new EmailMessage(from: "valid@email.com",
                to: "valid@email.com",
                subject: "A subject",
                message: "A message",
                blockedTerms: ["abc", "xyz"]);

            // Act
            var errors = emailMessage.Validate();

            // Assert
            errors.Should().BeEmpty();
        }
    }
}
