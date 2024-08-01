using Examples.Notification.Api.Domain.Models;
using System.Threading.Tasks;

namespace Examples.Notification.Api.Core.Services
{
    public interface IEmailSenderService
    {
        public Task SendAsync(EmailMessage message);
    }
}
