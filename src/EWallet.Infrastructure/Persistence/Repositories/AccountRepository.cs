using EWallet.Application.Common.Interfaces.Persistence;
using EWallet.Domain.Entities;
using EWallet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : BaseRepository<Account, int>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }

        protected override DbSet<Account> DbSet => DbContext.Accounts;

        protected override Task RemoveRelatedItemsAsync(Account entity) => Task.CompletedTask;
    }
}
