using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 表示用於表示股票查詢結果的類別。
/// </summary>
/// <param name="meta">元數據</param>
/// <param name="rows">股票實體集合</param>
internal class StocksResult(Meta meta, IEnumerable<StockEntity> rows)
    : AbstractPagingDataResult<IEnumerable<StockEntity>>(meta, rows);