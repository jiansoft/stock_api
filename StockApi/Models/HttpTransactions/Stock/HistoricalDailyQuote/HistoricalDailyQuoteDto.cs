using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

public class HistoricalDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe);