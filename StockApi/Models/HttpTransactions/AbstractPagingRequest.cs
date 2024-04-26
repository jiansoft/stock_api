using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 提供一個抽象基礎類別，用於分頁請求，包含頁碼和每頁記錄數的驗證。
/// </summary>
/// <param name="requestedPage">請求的頁碼。</param>
/// <param name="recordsPerPage">每頁的記錄數量。</param>
public abstract class AbstractPagingRequest(uint? requestedPage, uint? recordsPerPage) : IRequest
{
    /// <summary>
    /// 經過驗證的請求頁碼，確保頁碼有效。
    /// </summary>
    public uint RequestedPage { get; private set; } = ValidateRequestedPage(requestedPage);

    /// <summary>
    /// 經過驗證的每頁記錄數，確保數量在允許範圍內。
    /// </summary>
    public uint RecordsPerPage { get; private set; } = ValidateRecordsPerPage(recordsPerPage);

    /// <summary>
    /// 驗證請求頁碼是否有效，並返回有效的頁碼。
    /// </summary>
    /// <param name="requestedPage">待驗證的請求頁碼。</param>
    /// <returns>有效的請求頁碼。</returns>
    private static uint ValidateRequestedPage(uint? requestedPage)
    {
        return requestedPage is null or <= 0 ? Constants.DefaultPage : requestedPage.Value;
    }

    /// <summary>
    /// 驗證每頁記錄數是否有效，並返回有效的記錄數。
    /// </summary>
    /// <param name="recordsPerPage">待驗證的每頁記錄數。</param>
    /// <returns>有效的每頁記錄數。</returns>
    private static uint ValidateRecordsPerPage(uint? recordsPerPage)
    {
        return recordsPerPage is null or <= 0 or > Constants.MaximumRecordsPerPage
            ? Constants.DefaultRecordsPerPage
            : recordsPerPage.Value;
    }

    /// <summary>
    /// 抽象方法，由子類實現，用於生成包含特定前綴的唯一鍵值。
    /// </summary>
    /// <returns>格式化的具有前綴的唯一鍵值字串。</returns>
    public abstract string KeyWithPrefix();
}