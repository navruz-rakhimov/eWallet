
namespace EWallet.Infrastructure.Persistence.Initializer
{
    internal interface IDatabaseInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken cancellationToken);
    }
}
