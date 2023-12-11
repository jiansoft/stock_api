namespace StockApi.Models.HttpTransactions.Stock.Industry;

public class IndustriesResponse<T>: IResponse
{
    public int Code { get; set; }
    public Payload<T> Payload { get; set; }
    
    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesResponse<T>)}";
    }
}