namespace StockApi.Models.DataProviders;

/// <summary>
/// 泛型資料結果介面
/// </summary>
/// <typeparam name="T">結果的型別</typeparam>
public interface IDataResult<T>
{
    /// <summary>
    ///  取得或設定結果
    /// </summary>
    T Result { get; set; }
}