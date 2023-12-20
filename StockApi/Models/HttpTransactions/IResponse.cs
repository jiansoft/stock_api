namespace StockApi.Models.HttpTransactions
{
    public interface IResponse<T> : IHttpTransaction
    {
        public int Code { get; set; }

        public T Payload { get; set; }
    }
}
