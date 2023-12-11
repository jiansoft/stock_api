using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Industry;

public class IndustryDto(IndustryEntity ie)
{
    public int IndustryId { get; set; } = ie.IndustryId;

    public string Name { get; set; } = ie.Name;
}