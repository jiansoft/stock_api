namespace StockApi.Models.Entities;

/// <summary>
/// 設定實體
/// </summary>
public class ConfigEntity
{
    /// <summary>
    /// 取得或設定設定項目的鍵
    /// </summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// 取得或設定設定項目的值
    /// </summary>
    public string Val { get; set; } = string.Empty;
}