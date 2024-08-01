using Examples.Notification.Api.Core.Services;
using Examples.Notification.Api.Domain.Models;
using Examples.Notification.Api.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Application.Commands.SendEmail
{
    public class SendEmailCommandHandler : ISendEmailCommandHandler
    {
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly IBlockedTermsRepository _blockedTermsRepository;
        private readonly IEmailSenderService _emailSenderService;

        public SendEmailCommandHandler(ILogger<SendEmailCommandHandler> logger, IBlockedTermsRepository blockedTermsRepository, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _blockedTermsRepository = blockedTermsRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task<ProblemDetails?> HandleAsync(SendEmailCommand command)
        {
            _logger.LogInformation("Started the email sending process.");

            var blockedTerms = await _blockedTermsRepository.GetAllAsync();

            var message = new EmailMessage(command.From, command.To, command.Subject, command.Message, blockedTerms);

            var validationErrors = message.Validate();

            if (validationErrors.Any())
            {
                _logger.LogError("Completed the email sending process with errors: {@errors}", validationErrors);

                return ValidationError(validationErrors);
            }

            await _emailSenderService.SendAsync(message);

            _logger.LogInformation("Completed the email sending process successfully.");

            return null;
        }

        private static ProblemDetails ValidationError(IReadOnlyCollection<string> validationErrors)
        {
            var error = new ProblemDetails
            {
                Type = "invalid-values",
                Title = "Invalid values",
                Status = StatusCodes.Status400BadRequest,
                Detail = "The message contains invalid values.",
            };

            error.Extensions.Add("validationErrors", validationErrors);

            return error;
        }
    }
}
