namespace Visa.Cal.Abstraction.Domain;

public class SystemPortfolio : IHasId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SystemType { get; set; }
    public string TechnologyType { get; set; }
    
}