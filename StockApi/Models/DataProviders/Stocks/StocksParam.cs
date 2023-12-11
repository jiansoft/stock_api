using StockApi.Models.HttpTransactions.Stock.Details;

namespace StockApi.Models.DataProviders.Stocks;

public struct StocksParam : IKey
{
    private string BaseKey { get; set; }
    public long PageIndex { get; set; }
    public long PageSize { get; set; }

    public StocksParam(DetailsRequest req)
    {
        BaseKey = req.KeyWithPrefix();
        PageIndex = req.PageIndex;
        PageSize = req.PageSize;
    }

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(StocksParam)}:{PageIndex}-{PageSize}";
    }
}