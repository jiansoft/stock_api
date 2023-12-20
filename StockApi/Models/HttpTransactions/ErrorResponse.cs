namespace StockApi.Models.HttpTransactions
{
    public class ErrorResponse<T>(T msg) : IResponse <T>
    {
        public string KeyWithPrefix()
        {
            return $"{nameof(ErrorResponse<T>)}";
        }
        
        //public required string Message { get; set; }

        public int Code { get; set; }
        public T Payload { get; set; } = msg;

        
    }
}
