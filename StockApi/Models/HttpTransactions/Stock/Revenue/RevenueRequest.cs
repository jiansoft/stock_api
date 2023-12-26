namespace StockApi.Models.HttpTransactions.Stock.Revenue;

public struct RevenueRequest : IRequest
{
    public long PageIndex { get; set; }
    public long PageSize { get; set; }

    public long MonthOfYear { get; set; }
    public string StockSymbol { get; set; }
    
    public string KeyWithPrefix()
    {
        return $"{nameof(RevenueRequest)}:{StockSymbol}-{MonthOfYear}-{PageIndex}-{PageSize}";
    }
}