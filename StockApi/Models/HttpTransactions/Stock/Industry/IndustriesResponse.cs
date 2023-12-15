namespace StockApi.Models.HttpTransactions.Stock.Industry;

public class IndustriesResponse(IEnumerable<IndustryDto> data): IResponse
{
    public int Code { get; set; }
    public Payload<IEnumerable<IndustryDto>> Payload { get; set; } = new(data);

    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesResponse)}";
    }
}