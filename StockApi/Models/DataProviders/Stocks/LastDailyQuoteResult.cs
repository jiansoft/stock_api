using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

public class LastDailyQuoteResult<T>(Meta meta, T entities) : IDataResult<T>
{
    public Meta Meta { get; set; } = meta;

    public T Result { get; set; } = entities;
}