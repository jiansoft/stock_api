namespace StockApi.Models.HttpTransactions.Stock.Dividend;

public class DividendResponse<T> : IResponse
{
    public int Code { get; set; }
    public Payload<T> Payload { get; set; }

    public string KeyWithPrefix()
    {
        return $"{nameof(DividendResponse<T>)}";
    }
}