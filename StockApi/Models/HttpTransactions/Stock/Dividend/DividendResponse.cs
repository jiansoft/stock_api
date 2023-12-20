namespace StockApi.Models.HttpTransactions.Stock.Dividend;

public class DividendPayload<T>(T data) : IPayload<T>
{
    public T Data { get; set; } = data;
}

public class DividendResponse<T>(T data) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(DividendResponse<T>)}";
    }
    
    public int Code { get; set; }
    
    public T Payload { get; set; } = data;
}