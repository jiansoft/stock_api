using StockApi.Models.HttpTransactions;
using System.Collections;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 提供了一種泛型方式來封裝分頁數據的結果，包含分頁的元數據和數據實體集合。
/// 這個抽象類別可以被用於不同類型的數據集合，以支持分頁功能，
/// 例如在網頁應用或API響應中顯示數據的子集。
/// </summary>
internal abstract class AbstractPagingDataResult<T>(Meta meta, T rows) : IDataResult<T> where T : IEnumerable
{
    /// <summary>
    /// 分頁的元數據，提供了有關數據集合分頁的詳細信息，例如當前頁碼、每頁大小和總頁數。
    /// </summary>
    public Meta Meta { get; } = meta;

    /// <inheritdoc />
    public T Rows { get; set; } = rows;
}
