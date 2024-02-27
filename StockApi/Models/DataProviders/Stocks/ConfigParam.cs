namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表組態參數的結構
/// </summary>
/// <param name="key"></param>
internal struct ConfigParam(string key) : IKey
{
    /// <summary>
    /// 取得或設定組態參數的鍵
    /// </summary>
    public string Key { get; set; } = key;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(ConfigParam)}:{Key}";
    }
}