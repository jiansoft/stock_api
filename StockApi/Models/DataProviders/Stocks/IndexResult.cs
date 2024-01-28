using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表索引結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="entities"></param>
public class IndexResult(Meta meta, IEnumerable<IndexEntity> entities)
    : AbstractPagingDataResult<IEnumerable<IndexEntity>>(meta, entities);
