using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infra.Data.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
              .IsRequired()
              .HasColumnName("UserId");

            builder.Property(c => c.Balance)
              .IsRequired()
              .HasColumnName("Balance");

            builder.Property(c => c.CreditLimit)
              .IsRequired()
              .HasColumnName("CreditLimit");
        }
    }
}
