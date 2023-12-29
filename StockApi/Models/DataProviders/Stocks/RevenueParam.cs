using StockApi.Models.HttpTransactions.Stock.Revenue;

namespace StockApi.Models.DataProviders.Stocks;

public struct RevenueParam(RevenueRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public long PageIndex { get; set; } = req.PageIndex;
    public long PageSize { get; set; } = req.PageSize;
    public long MonthOfYear { get; set; } = req.MonthOfYear;
    public string StockSymbol { get; set; } = req.StockSymbol;

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(RevenueParam)}:{StockSymbol}-{MonthOfYear}-{PageIndex}-{PageSize}";
    }
}