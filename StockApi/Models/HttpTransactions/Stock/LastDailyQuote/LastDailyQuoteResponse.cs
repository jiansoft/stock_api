using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

public struct LastDailyQuotePayload(string date, IEnumerable<LastDailyQuoteDto> data)
{
    public string Date { get; set; } = date;
    
    public IEnumerable<LastDailyQuoteDto> Data { get; set; } = data;
}

public class LastDailyQuoteResponse(Meta meta, LastDailyQuotePayload payload) : IResponse
{
    /// <summary>
    /// 分頁相關資料
    /// </summary>
    public Meta Meta { get; set; } = meta;
    
    public Payload<LastDailyQuotePayload> Payload { get; set; } = new(payload);
    public string KeyWithPrefix()
    {
        return $"{nameof(LastDailyQuoteResponse)}";
    }

    public int Code { get; set; }
}