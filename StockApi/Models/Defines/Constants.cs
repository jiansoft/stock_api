namespace StockApi.Models.Defines;

/// <summary>
/// 包含應用程式中使用的常數值的抽象類別。
/// </summary>
public abstract class Constants
{
    /// <summary>
    /// 0
    /// </summary>
    public const int Zero = 0;
    /// <summary>
    /// 預設第一頁
    /// </summary>
    public const int DefaultRequestedPage = 1;

    /// <summary>
    /// 預設每頁30筆
    /// </summary>
    public const int DefaultRecordsPerPage = 30;

    /// <summary>
    /// 每頁最多1000筆
    /// </summary>
    public const int MaximumRecordsPerPage = 1000;

    /// <summary>
    /// 用於檢索最後收盤日期的關鍵字。
    /// </summary>
    public const string KeyLastClosingKay = "last-closing-day";
}