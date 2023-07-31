using EWallet.Application.Common.Interfaces.Persistence;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Domain.Entities;
using EWallet.Infrastructure.Identity;
using EWallet.Infrastructure.Persistence.Context;
using EWallet.Infrastructure.Persistence.Initializer;
using EWallet.Infrastructure.Persistence.Repositories;
using EWallet.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EWallet.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration)
                .AddIdentity(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var currentAssemblyName = typeof(ApplicationDbContext).Assembly.FullName;

                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly(currentAssemblyName);
                });
            });

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            // todo: add seeder services

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        
        private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole<int>>(options =>
                {
                    var identityConfig = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();

                    options.SignIn.RequireConfirmedAccount = identityConfig!.RequireConfirmedAccount;
                    options.Password.RequireDigit = identityConfig.Password.RequireDigit;
                    options.Password.RequireLowercase = identityConfig.Password.RequireLowercase;
                    options.Password.RequiredLength = identityConfig.Password.RequiredLength;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider
                .GetRequiredService<IDatabaseInitializer>()
                .InitializeDatabaseAsync(cancellationToken);
        }
    }
}
