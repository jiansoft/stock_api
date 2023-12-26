namespace StockApi.Models.HttpTransactions.Stock.Revenue;

public class RevenueResponse<T>(T payload) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(RevenueResponse<T>)}";
    }

    public int Code { get; set; }

    public T Payload { get; set; } = payload;
}