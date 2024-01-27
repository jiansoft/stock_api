using StockApi.Models.HttpTransactions.Stock.Dividend;

namespace StockApi.Models.DataProviders.Stocks;

internal readonly struct DividendParam(DividendRequest req) : IKey
{
    public string StockSymbol { get; } = req.StockSymbol;
    private string BaseKey { get; } = req.KeyWithPrefix();

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndustriesParam)}:{StockSymbol}";
    }
}