namespace StockApi.Models.HttpTransactions.Stock.Dividend;

public class DividendRequest(string stockSymbol) : IRequest
{
    public string StockSymbol { get; set; } = stockSymbol;
    
    public string KeyWithPrefix()
    {
        return $"{nameof(DividendRequest)}:{StockSymbol}";
    }
}