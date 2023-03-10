namespace Visa.Cal.Abstraction.Domain;

public class SystemPortfolio : IHasId
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SystemType { get; set; } = null!;
    public string TechnologyType { get; set; } = null!;
    
}