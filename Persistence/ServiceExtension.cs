using Application.Interfaces;
using Application.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Authentication;
using Persistence.Services;


namespace Persistence
{
    public static class ServiceExtension
    {
        public static void AddInfraestructureLayer(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeService, DataTimeService>();
        }
    }
}
