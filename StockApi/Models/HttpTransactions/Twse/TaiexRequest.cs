namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 用於處理台灣加權股價指數請求的類別，包含頁面索引和每頁記錄數的參數。
/// </summary>
/// <param name="page">請求的頁碼，指明要取得第幾頁的數據。</param>
/// <param name="recordsPerPage">每頁的記錄數，指定每頁顯示多少條數據。</param>
public class TaiexRequest (uint? requestedPage, int? recordsPerPage)
    : AbstractPagingRequest(requestedPage, recordsPerPage)
{
    /// <summary>
    /// 
    /// </summary>
    public string Category => "TAIEX";
    
    /// <summary>
    /// 生成包含前綴的唯一鍵值，用於識別此分頁請求。
    /// </summary>
    /// <returns>格式化的字符串，包含請求的分頁信息。</returns>
    public override string KeyWithPrefix()
    {
        return $"{nameof(TaiexRequest)}:{RequestedPage}-{RecordsPerPage}";
    }
}
