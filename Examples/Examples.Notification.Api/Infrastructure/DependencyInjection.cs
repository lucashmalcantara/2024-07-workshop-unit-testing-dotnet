using Examples.Notification.Api.Core.Services;
using Examples.Notification.Api.Domain.Repositories;
using Examples.Notification.Api.Infrastructure.Repositories;
using Examples.Notification.Api.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.Notification.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBlockedTermsRepository, BlockedTermsRepository>();

            return services;
        }
    }
}
