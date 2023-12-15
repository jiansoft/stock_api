namespace StockApi.Models.HttpTransactions.Stock.Dividend;

public class DividendResponse(IEnumerable<DividendDto> data) : IResponse
{
    public int Code { get; set; }
    public Payload<IEnumerable<DividendDto>> Payload { get; set; } = new(data);

    public string KeyWithPrefix()
    {
        return $"{nameof(DividendResponse)}";
    }
}