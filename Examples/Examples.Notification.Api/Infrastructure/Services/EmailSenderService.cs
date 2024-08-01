using Examples.Notification.Api.Core.Services;
using Examples.Notification.Api.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Infrastructure.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(EmailMessage message)
        {
            _logger.LogInformation("Email sent successfully: From: {From} -> To: {To} | Subject: {Subject}", message.From, message.To, message.Subject);

            return Task.CompletedTask;
        }
    }
}
