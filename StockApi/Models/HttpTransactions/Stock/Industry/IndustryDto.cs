using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Industry;

/// <summary>
/// 表示用於表示產業數據的數據傳輸對象（DTO）類別。
/// </summary>
/// <param name="ie">產業實體</param>
public class IndustryDto(StockIndustryEntity ie)
{
    /// <summary>
    /// 初始化 <see cref="IndustryDto"/> 類別的新執行個體。
    /// </summary>
    public IndustryDto():this(new StockIndustryEntity())
    {
    }
    
    /// <summary>
    /// 產業編號
    /// </summary>
    public int IndustryId { get; set; } = ie.IndustryId;

    /// <summary>
    /// 產業名稱
    /// </summary>
    public string Name { get; set; } = ie.Name;
}