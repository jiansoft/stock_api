namespace StockApi.Models.HttpTransactions.Stock.Revenue;

/// <summary>
/// 
/// </summary>
/// <param name="payload"></param>
/// <typeparam name="T"></typeparam>
public class RevenueResponse<T>(T payload) : IResponse<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string KeyWithPrefix()
    {
        return $"{nameof(RevenueResponse<T>)}";
    }

    /// <summary>
    /// 
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public T Payload { get; set; } = payload;
}