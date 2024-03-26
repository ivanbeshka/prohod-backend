using Microsoft.EntityFrameworkCore;
using Prohod.Domain.AggregationRoot;
using Prohod.Domain.Forms;
using Prohod.Domain.Users;
using Prohod.Domain.VisitRequests;
using Prohod.Infrastructure.Accounts.Models;
using Prohod.Infrastructure.Forms;
using Prohod.Infrastructure.VisitRequests;

namespace Prohod.Infrastructure.Database;

public sealed class PostgresDbContext : DbContext, IAppDbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; } = default!;
    
    public DbSet<Form> Forms { get; set; } = default!;

    public DbSet<Account> Accounts { get; set; } = default!;

    public DbSet<VisitRequest> VisitRequests { get; set; } = default!;
    
    public new DbSet<T> Set<T>()
        where T : class, IAggregationRoot 
        => base.Set<T>();

    public async Task<int> SaveChangesAsync()
        => await base.SaveChangesAsync().ConfigureAwait(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Role>();
        modelBuilder.HasPostgresEnum<VisitRequestStatus>();
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FormEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VisitRequestEntityConfiguration());
    }
}