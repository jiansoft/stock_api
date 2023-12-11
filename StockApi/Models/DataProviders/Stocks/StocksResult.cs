using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

public class StocksResult(Meta meta, IEnumerable<StockEntity> entities)
{
    public Meta Meta { get; set; } = meta;
    public IEnumerable<StockEntity> Entities { get; set; } = entities;
}