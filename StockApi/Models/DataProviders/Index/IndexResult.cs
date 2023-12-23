using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Index;

public class IndexResult(Meta meta, IEnumerable<IndexEntity> entities)
{
    public Meta Meta { get; set; } = meta;
    public IEnumerable<IndexEntity> Entities { get; set; } = entities;
}