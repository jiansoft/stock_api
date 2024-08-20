namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個抽象的分頁回應類，用於封裝分頁資料的通用回應結構。
/// </summary>
/// <typeparam name="T">表示回應負載的類型。</typeparam>
public abstract class AbstractPagingResponse<T>(int code, T payload) : IResponse<T>
{
    /// <summary>
    /// 獲取帶有前綴的鍵，該方法應在派生類中實現。
    /// </summary>
    /// <returns>帶有前綴的鍵。</returns>
    public abstract string KeyWithPrefix();
    
    /// <summary>
    /// 獲取或設定狀態碼。
    /// </summary>
    public int Code { get; set; } = code;

    /// <summary>
    /// 獲取或設定回應的資料。
    /// </summary>
    public T Payload { get; set; } = payload;
}