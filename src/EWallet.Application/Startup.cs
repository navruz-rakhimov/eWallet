using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EWallet.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssembly = typeof(Startup).Assembly;

            services.AddValidatorsFromAssembly(currentAssembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(currentAssembly));
            
            return services;
        }
    }
}
