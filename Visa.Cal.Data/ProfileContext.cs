using Microsoft.EntityFrameworkCore;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Data;

public class ProfileContext : DbContext
{
    public ProfileContext() : base()
    {
    }

    public ProfileContext(DbContextOptions options) : base(options: options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder: optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<SystemPortfolio>();
        entity.HasIndex(indexExpression: x => x.Name);
        entity.HasKey(keyExpression: x=>x.Id);
    }


    public static ProfileContext Create(string? connectionString = default)
    {
        connectionString ??= $"name={nameof(ProfileContext)}";
        
        var builder = new DbContextOptionsBuilder<ProfileContext>()
            .UseSqlServer(connectionString: connectionString);
        
        var instance = new ProfileContext(options: builder.Options);

        return instance;
    }
}