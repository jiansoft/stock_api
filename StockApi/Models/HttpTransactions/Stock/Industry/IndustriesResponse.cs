namespace StockApi.Models.HttpTransactions.Stock.Industry;

public class IndustriesPayload<T>(T data) : IPayload<T>
{
    public T Data { get; set; } = data;
}

public class IndustriesResponse<T>(T data): IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesResponse<T>)}";
    }
    
    public int Code { get; set; }
    
    public T Payload { get; set; } = data;
}