using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

public class IndustriesResult(IEnumerable<IndustryEntity> entities)
{
    public IEnumerable<IndustryEntity> Entities { get; set; } = entities;
}