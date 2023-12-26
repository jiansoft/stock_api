namespace StockApi.Models.HttpTransactions.Stock.Details
{
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
