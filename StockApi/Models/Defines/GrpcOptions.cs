namespace StockApi.Models.Defines;

/// <summary>
/// 
/// </summary>
public class GrpcOptions
{
    /// <summary>
    /// 
    /// </summary>
    public int RustGrpcTargetPort { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string HttpProtocol { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public string RustGrpcTarget { get; set; } = string.Empty;
}