using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Models.DataProviders.Stocks;

public struct IndexParam(TaiexRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public long PageIndex { get; set; } = req.PageIndex;
    public long PageSize { get; set; } = req.PageSize;
    
    public string Category { get; set; } = "TAIEX";

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndexParam)}:{PageIndex}-{PageSize}";
    }
}