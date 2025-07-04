using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Core.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMember> ChannelMembers { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Default);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}