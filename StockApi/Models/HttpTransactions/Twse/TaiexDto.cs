using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 代表台灣加權股價指數的資料傳輸對象，用於封裝指數實體的相關資訊。
/// </summary>
/// <param name="ie">指數實體，包含台灣加權股價指數的資料。</param>
public class TaiexDto(IndexEntity ie)
{
    /// <summary>
    /// 代表指數的日期。
    /// </summary>
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(ie.Date);

    /// <summary>
    /// 代表當日交易量。
    /// </summary>
    public decimal TradingVolume { get; set; } = ie.TradingVolume;

    /// <summary>
    /// 代表交易筆數。
    /// </summary>
    public decimal Transaction { get; set; } = ie.Transaction;

    /// <summary>
    /// 代表交易總值。
    /// </summary>
    public decimal TradeValue { get; set; } = ie.TradeValue;

    /// <summary>
    /// 代表指數變動點數。
    /// </summary>
    public decimal Change { get; set; } = ie.Change;

    /// <summary>
    /// 代表當日指數值。
    /// </summary>
    public decimal Index { get; set; } = ie.Index;
}
