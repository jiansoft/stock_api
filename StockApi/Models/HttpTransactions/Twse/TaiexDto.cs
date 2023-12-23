using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Twse;

public class TaiexDto(IndexEntity ie)
{
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(ie.Date);
    public decimal TradingVolume { get; set; } = ie.TradingVolume;
    public decimal Transaction { get; set; } = ie.Transaction;
    public decimal TradeValue { get; set; } = ie.TradeValue;
    public decimal Change { get; set; } = ie.Change;
    public decimal Index { get; set; } = ie.Index;
}