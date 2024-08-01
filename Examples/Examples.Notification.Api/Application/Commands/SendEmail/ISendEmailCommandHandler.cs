using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Application.Commands.SendEmail
{
    public interface ISendEmailCommandHandler
    {
        public Task<ProblemDetails?> HandleAsync(SendEmailCommand command);
    }
}
