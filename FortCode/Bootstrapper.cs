using FortCode.Repository.FortRepository;
using FortCode.Repository.Interfaces;
using FortCode.Service;
using FortCode.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Helpers;

namespace FortCode
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(configuration.GetConnectionString("DbContext").ToString()));
            services.AddSingleton<IFortService, FortService>();
            services.AddSingleton<IFortRepository, FortRepository>();
            services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }
    }
}
