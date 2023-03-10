using Microsoft.AspNetCore.Mvc;
using Visa.Cal.Abstraction.Data;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemProfileController : ControllerBase
{
    private readonly IRepositoryGeneric<SystemPortfolio> _repository;
    private readonly ILogger<SystemProfileController> _logger;

    public SystemProfileController(
        IRepositoryGeneric<SystemPortfolio> repository,
        ILogger<SystemProfileController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<SystemPortfolio>> Get(string? name, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Get");
        if (name == null)
        {
            return 
                await _repository.FetchAsync(x => x.Id>=0, cancellationToken);
        }
        
        return 
            await _repository.FetchAsync(x => x.Name == name, cancellationToken);
    }
    
    [HttpPost]
    public async Task Post(SystemPortfolio item, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Post");

        await _repository.AddOrUpdateAsync(item, cancellationToken);
    }
    
    [HttpPut]
    public async Task Put(SystemPortfolio item, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Put");

        await _repository.AddOrUpdateAsync(item, cancellationToken);
    }
    
    [HttpDelete]
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Delete");

        var item = new SystemPortfolio
        {
            Id = id
        };
        
        await _repository.DeleteAsync(item, cancellationToken);
    }
    
    
}