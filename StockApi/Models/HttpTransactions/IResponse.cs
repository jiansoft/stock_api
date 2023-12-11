namespace StockApi.Models.HttpTransactions
{
    public interface IResponse: IHttpTransaction
    {
        public int Code { get; set; }
    }
}
