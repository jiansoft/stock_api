using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表最後每日報價結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="rows"></param>
internal class LastDailyQuoteResult(Meta meta, IEnumerable<LastDailyQuoteEntity> rows) 
    : AbstractPagingDataResult<IEnumerable<LastDailyQuoteEntity>>(meta, rows);