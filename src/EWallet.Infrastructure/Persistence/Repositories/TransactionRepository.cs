using EWallet.Application.Common.Interfaces.Persistence;
using EWallet.Domain.Entities;
using EWallet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Transaction> DbSet => DbContext.Transactions;

        protected override Task RemoveRelatedItemsAsync(Transaction entity) => Task.CompletedTask;
    }
}
