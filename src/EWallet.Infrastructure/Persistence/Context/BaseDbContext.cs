using EWallet.Domain.Common;
using EWallet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Infrastructure.Persistence.Context
{
    public class BaseDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BaseDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            HandleAuditingBeforeSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        private void HandleAuditingBeforeSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity<int>>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTimeOffset.UtcNow;
                        break;

                    // we can add here soft delete, etc.

                    default: continue;
                }
            }
        }
    }
}
