namespace Examples.Notification.Api.Application.Commands.SendEmail
{
    public record SendEmailCommand(string From, string To, string Subject, string Message);
}
