using EWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWallet.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.FromAccount)
                .WithMany(o => o.Withdrawals)
                .HasForeignKey(x => x.FromAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.ToAccount)
                .WithMany(o => o.Deposits)
                .HasForeignKey(x => x.ToAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
