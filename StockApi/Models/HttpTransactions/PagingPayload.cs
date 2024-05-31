namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 提供分頁功能的結構，包含元數據和數據集合。
/// </summary>
/// <param name="Meta">分頁的元數據。</param>
/// <param name="Data">數據集合。</param>
/// <typeparam name="T">數據的類型。</typeparam>
    internal record PagingPayload<T>(Meta Meta, IEnumerable<T> Data) : IPagingPayload<T>
    {
        /// <inheritdoc />
        public Meta Meta { get; set; } = Meta;

        /// <inheritdoc />
        public IEnumerable<T> Data { get; set; } = Data;
    }