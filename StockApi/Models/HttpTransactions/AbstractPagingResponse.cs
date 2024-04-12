namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AbstractPagingResponse<T>(int code, T payload) : IResponse<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public abstract string KeyWithPrefix();
    
    /// <summary>
    /// 
    /// </summary>
    public int Code { get; set; } = code;

    /// <summary>
    /// 
    /// </summary>
    public T Payload { get; set; } = payload;
}