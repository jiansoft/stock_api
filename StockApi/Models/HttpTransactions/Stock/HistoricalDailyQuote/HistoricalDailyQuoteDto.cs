using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

/// <summary>
/// 
/// </summary>
/// <param name="dqe"></param>
public class HistoricalDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe);