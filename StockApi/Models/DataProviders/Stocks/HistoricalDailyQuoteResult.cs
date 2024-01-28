using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表歷史每日報價結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="entities"></param>
/// <typeparam name="T"></typeparam>
public class HistoricalDailyQuoteResult(Meta meta, IEnumerable<HistoricalDailyQuoteEntity> entities) 
    : AbstractPagingDataResult<IEnumerable<HistoricalDailyQuoteEntity>>(meta, entities);