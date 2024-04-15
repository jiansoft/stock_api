using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Entities;

/// <summary>
/// 設定實體
/// </summary>
public class ConfigEntity : IEntity
{
    /// <summary>
    /// 取得或設定設定項目的鍵
    /// </summary>
    [MaxLength(64)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 取得或設定設定項目的值
    /// </summary>
    [MaxLength(256)]
    public string Val { get; set; } = string.Empty;
}