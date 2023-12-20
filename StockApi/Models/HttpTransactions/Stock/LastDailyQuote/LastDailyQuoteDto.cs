using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.DtoBase;

namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

public class LastDailyQuoteDto(DailyQuoteEntity dqe) : DailyQuoteDtoBase(dqe);