namespace StockApi.Models.HttpTransactions.Stock.Details
{
    public class DetailsPayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
    {
        /// <summary>
        /// 分頁相關資料
        /// </summary>
        public Meta Meta { get; set; } = meta;

        public IEnumerable<T> Data { get; set; } = data;
    }

    public class DetailsResponse<T>(T payload) : IResponse<T>
    {
        public string KeyWithPrefix()
        {
            return $"{nameof(DetailsResponse<T>)}";
        }

        public int Code { get; set; }
        
        public T Payload { get; set; } = payload;
    }
}
