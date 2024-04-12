using System.Collections;

namespace StockApi.Models.HttpTransactions.Stock.Industry;

/// <summary>
/// 用於封裝股票產業分類相關數據的負載類別。
/// </summary>
/// <param name="data">包含股票產業分類數據的對象。</param>
/// <typeparam name="T">股票產業分類數據的類型。</typeparam>
public class IndustriesPayload<T>(T data) : IPayload<T> where T : IEnumerable
{
    /// <inheritdoc />
    public T Data { get; set; } = data;
}

/// <summary>
/// 用於封裝股票產業分類請求的回應類別。
/// </summary>
/// <typeparam name="T">股票產業分類回應數據的類型。</typeparam>
public class IndustriesResponse<T>(int code, T payload) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesResponse<T>)}-{typeof(T).Name}-{Code}";
    }

    /// <summary>
    /// 
    /// </summary>
    public int Code { get; set; } = code;
    
    /// <summary>
    /// 
    /// </summary>
    public T Payload { get; set; } = payload;
}