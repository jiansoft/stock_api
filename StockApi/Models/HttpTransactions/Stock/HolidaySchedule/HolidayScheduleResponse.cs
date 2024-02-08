namespace StockApi.Models.HttpTransactions.Stock.HolidaySchedule;

/// <summary>
/// 表示用於獲取股票市場休市日資訊的回應類別。
/// </summary>
/// <param name="data"></param>
/// <typeparam name="T"></typeparam>
public class HolidaySchedulePayload<T>(T data) : IPayload<T>
{
    /// <inheritdoc />
    public T Data { get; set; } = data;
}

/// <summary>
/// 
/// </summary>
/// <param name="data"></param>
/// <typeparam name="T"></typeparam>
public class HolidayScheduleResponse<T>(T data) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(HolidayScheduleResponse<T>)}";
    }

    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public T Payload { get; set; } = data;
}
