using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

/// <summary>
/// 表示一個最後每日報價的資料傳輸物件。
/// </summary>
/// <param name="dqe"></param>
public class LastDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe);