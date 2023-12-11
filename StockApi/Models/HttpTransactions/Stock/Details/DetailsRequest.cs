namespace StockApi.Models.HttpTransactions.Stock.Details
{
    public struct DetailsRequest : IRequest
    {
        public long PageIndex { get; set; }
        public long PageSize { get; set; }
        public string KeyWithPrefix()
        {
            return $"{nameof(DetailsRequest)}:{PageIndex}-{PageSize}";
        }
    }
    
   
}
