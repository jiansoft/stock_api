using StockApi.Models.HttpTransactions.Stock.Industry;

namespace StockApi.Models.DataProviders.Stocks;

public struct IndustriesParam(IKey req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndustriesParam)}";
    }
}