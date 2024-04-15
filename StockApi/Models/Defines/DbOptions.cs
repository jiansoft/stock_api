namespace StockApi.Models.Defines;

/// <summary>
/// 
/// </summary>
public record DbOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Connection { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string ProviderName { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public int CommandTimeout { get; set; }
}
