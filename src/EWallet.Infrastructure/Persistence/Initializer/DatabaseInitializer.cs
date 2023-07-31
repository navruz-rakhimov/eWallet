using EWallet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EWallet.Infrastructure.Persistence.Initializer
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(
            ApplicationDbContext dbContext,
            ILogger<DatabaseInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    try
                    {
                        _logger.LogInformation("Applying Database Migrations");

                        await _dbContext.Database.MigrateAsync(cancellationToken);

                        _logger.LogInformation("Migrations completed successfully");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"DB Migration failed with error message: {e.Message}");
                        throw;
                    }
                }

                // apply seeding
            }
        }
    }
}
