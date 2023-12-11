namespace StockApi.Models.HttpTransactions
{
    public class ErrorResponse : IResponse
    {
        public required string Message { get; set; }

        public int Code { get; set; }
        
        public string KeyWithPrefix()
        {
            return $"{nameof(ErrorResponse)}";
        }
    }
}
