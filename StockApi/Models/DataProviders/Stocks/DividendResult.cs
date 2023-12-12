using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

public class DividendResult(IEnumerable<DividendEntity> entities)
{
    public IEnumerable<DividendEntity> Entities { get; set; } = entities;
}