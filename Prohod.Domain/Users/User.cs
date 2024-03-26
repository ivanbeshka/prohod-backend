using Prohod.Domain.AggregationRoot;

namespace Prohod.Domain.Users;

public class User : IAggregationRoot
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Surname { get; private set; }
    
    public string UserEmail { get; private set; }
    
    public Role Role { get; private set; }
    
#pragma warning disable CS8618
    protected User() { }
#pragma warning restore CS8618
    
    public User(string name, string surname, string userEmail, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        UserEmail = userEmail;
        Role = role;
    }
}