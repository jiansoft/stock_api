using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

public class LastDailyQuoteResult(Meta meta, IEnumerable<LastDailyQuoteEntity> entities) : IDataResult<LastDailyQuoteEntity>
{
    public Meta Meta { get; set; } = meta;
    public IEnumerable<LastDailyQuoteEntity> Entities { get; set; } = entities;
}