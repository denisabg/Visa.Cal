using Visa.Cal.Abstraction.Data;
using Visa.Cal.Abstraction.Domain;
using Xunit;

namespace Visa.Cal.Data.Tests;

public class RepositoryTests
{

    [Fact]
    public async Task AddItemTest()
    {
        var item = new SystemPortfolio()
        {
            Name = "Xxxxxxxxxxxx",
            SystemType = "Web",
            TechnologyType = "angular"
        };

        var repository = CreateRepository();

        await repository.AddOrUpdateAsync(item);
        
        Assert.True(item.Id > 0);
    }

    private ProfileContext CreateContext()
    {
        var instance = ProfileContext.Create(ConnectionString);
        
        instance.Database.EnsureDeleted();
        instance.Database.EnsureCreated();

        return instance;
    }

    private IRepositoryGeneric<SystemPortfolio> CreateRepository()
    {
        var dbContext = CreateContext();
        var instance = new RepositoryGeneric<SystemPortfolio>(dbContext);

        return instance;
    }
    
    private const string ConnectionString = 
        @"Server=127.0.0.1\mssqllocldb,1433;Database=ProfileContextTest;User=sa;Password=Abgo!Den!1977;TrustServerCertificate=True";

}
