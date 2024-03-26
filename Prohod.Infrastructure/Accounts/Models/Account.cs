using Prohod.Domain.AggregationRoot;
using Prohod.Domain.Users;

namespace Prohod.Infrastructure.Accounts.Models;

public class Account : IAggregationRoot
{
    public Guid Id { get; private set; }

    public virtual User AssociatedUser { get; private set; }
    
    public string Login { get; private set; }
    
    public string PasswordHash { get; private set; }

#pragma warning disable CS8618
    protected Account() { }
#pragma warning restore CS8618

    public Account(User associatedUser, string login, string passwordHash)
    {
        AssociatedUser = associatedUser;
        Login = login;
        PasswordHash = passwordHash;
    }
}