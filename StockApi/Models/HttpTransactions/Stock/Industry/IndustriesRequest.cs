namespace StockApi.Models.HttpTransactions.Stock.Industry;

public struct IndustriesRequest : IRequest
{
    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesRequest)}";
    }
}