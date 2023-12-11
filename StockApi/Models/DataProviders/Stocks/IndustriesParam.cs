using StockApi.Models.HttpTransactions.Stock.Industry;

namespace StockApi.Models.DataProviders.Stocks;

public struct IndustriesParam : IKey
{
    private string BaseKey { get; set; }

    public IndustriesParam(IndustriesRequest req)
    {
        BaseKey = req.KeyWithPrefix();
    }

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndustriesParam)}";
    }
}