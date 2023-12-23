namespace StockApi.Models.Entities;

public class IndexEntity
{
    public DateTime Date { get; set; }
    public decimal TradingVolume { get; set; }
    public decimal Transaction { get; set; }
    public decimal TradeValue { get; set; }
    public decimal Change { get; set; }
    public decimal Index { get; set; }
}