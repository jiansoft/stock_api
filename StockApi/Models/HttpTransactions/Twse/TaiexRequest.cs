namespace StockApi.Models.HttpTransactions.Twse;

public class TaiexRequest :IKey
{
    public long PageIndex { get; set; }
    public long PageSize { get; set; }

    public string KeyWithPrefix()
    {
        return $"{nameof(TaiexRequest)}:{PageIndex}-{PageSize}";
    }
}