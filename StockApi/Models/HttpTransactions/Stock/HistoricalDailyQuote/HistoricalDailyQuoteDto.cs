using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

/// <summary>
/// 
/// </summary>
/// <param name="dqe"></param>
public class HistoricalDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe)
{
    /// <summary>
    /// 初始化 <see cref="HistoricalDailyQuoteDto"/> 類別的新執行個體。
    /// </summary>
    public HistoricalDailyQuoteDto() : this(new DailyQuoteEntity())
    {
    }
}