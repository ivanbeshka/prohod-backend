using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prohod.Domain.Users;

namespace Prohod.Infrastructure.Accounts.Models;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    private const string AssociatedUserId = nameof(Account.AssociatedUser) + nameof(User.Id);
    
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasIndex(account => account.Login)
            .IsUnique();

        builder
            .HasIndex(AssociatedUserId)
            .IsUnique();
    }
}