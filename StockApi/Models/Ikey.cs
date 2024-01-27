namespace StockApi.Models;

/// <summary>
/// 定義一個包含鍵值生成方法的介面。此介面用於實現類別，以生成具有特定前綴的唯一鍵值。
/// </summary>
public interface IKey
{
    /// <summary>
    /// 生成並返回一個包含特定前綴的唯一鍵值。此方法用於識別並區分不同的數據請求或實體。
    /// </summary>
    /// <returns>格式化的具有前綴的唯一鍵值字串。</returns>
    public string KeyWithPrefix();
}