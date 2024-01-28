using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

/// <summary>
/// 表示一個最後每日報價的資料傳輸物件。
/// </summary>
/// <param name="dqe"></param>
public class LastDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe)
{
    /// <summary>
    /// 初始化 <see cref="LastDailyQuoteDto"/> 類別的新執行個體。
    /// </summary>
    public LastDailyQuoteDto() : this(new DailyQuoteEntity())
    {
    }
}