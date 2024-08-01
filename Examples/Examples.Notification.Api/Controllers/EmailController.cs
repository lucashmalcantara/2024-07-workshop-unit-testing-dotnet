using Examples.Notification.Api.Application.Commands.SendEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Controllers
{
    [ApiController]
    [Route("email")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAsync(SendEmailCommand command, [FromServices] ISendEmailCommandHandler handler)
        {
            var error = await handler.HandleAsync(command);

            if (error != null)
                return StatusCode(error.Status!.Value, error);

            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}
