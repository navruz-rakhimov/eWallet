using EWallet.Application.Common.Interfaces.Persistence;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Infrastructure.Persistence.Context;

namespace EWallet.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IAccountRepository AccountRepository { get; protected set; }
        public ITransactionRepository TransactionRepository { get; protected set; }

        public UnitOfWork(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
            TransactionRepository = transactionRepository;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
