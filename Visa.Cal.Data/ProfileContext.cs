using Microsoft.EntityFrameworkCore;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Data;

public class ProfileContext : DbContext
{
    public ProfileContext() : base()
    {

    }

    public ProfileContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<SystemPortfolio>();
        entity.HasIndex(x => x.Name);
        entity.HasKey(x=>x.Id);
    }


    public static ProfileContext Create(string? connectionString = default)
    {
        connectionString ??= $"name={nameof(ProfileContext)}";
        
        var builder = new DbContextOptionsBuilder<ProfileContext>()
            .UseSqlServer(connectionString);
        
        var instance = new ProfileContext(builder.Options);

        return instance;
    }
}