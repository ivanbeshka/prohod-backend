namespace Prohod.Infrastructure.Accounts.Passwords.Options;

public record PasswordsSalt
{
    public required string Value { get; init; }
}