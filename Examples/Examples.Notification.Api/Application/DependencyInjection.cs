using Examples.Notification.Api.Application.Commands.SendEmail;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.Notification.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ISendEmailCommandHandler, SendEmailCommandHandler>();

            return services;
        }
    }
}
