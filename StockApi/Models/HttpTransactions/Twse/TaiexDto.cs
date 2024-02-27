namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 代表台灣加權股價指數的資料傳輸對象，用於封裝指數實體的相關資訊。
/// </summary>
public class TaiexDto
{
    /// <summary>
    /// 
    /// </summary>
    public TaiexDto()
    {
        
    }
    
    /// <summary>
    /// 代表指數的日期。
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// 代表當日交易量。
    /// </summary>
    public decimal TradingVolume { get; set; } 

    /// <summary>
    /// 代表交易筆數。
    /// </summary>
    public decimal Transaction { get; set; } 

    /// <summary>
    /// 代表交易總值。
    /// </summary>
    public decimal TradeValue { get; set; } 

    /// <summary>
    /// 代表指數變動點數。
    /// </summary>
    public decimal Change { get; set; } 

    /// <summary>
    /// 代表當日指數值。
    /// </summary>
    public decimal Index { get; set; } 
}
