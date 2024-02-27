using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表最後每日報價結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="entities"></param>
internal class LastDailyQuoteResult(Meta meta, IEnumerable<LastDailyQuoteEntity> entities) 
    : AbstractPagingDataResult<IEnumerable<LastDailyQuoteEntity>>(meta, entities);