using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Details;

public class DetailDto(StockEntity se)
{
    /// <summary>
    /// Gets or sets the stock symbol.
    /// </summary>
    /// <value>
    /// The stock symbol.
    /// </value>
    public string StockSymbol { get; set; } = se.StockSymbol;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; } = se.Name;

    /// <summary>
    /// Gets or sets the last one EPS (Earnings Per Share).
    /// </summary>
    public decimal LastOneEps { get; set; } = se.LastOneEps;

    /// <summary>
    /// Gets or sets the value of the last four earning per share (EPS).
    /// </summary>
    public decimal LastFourEps { get; set; } = se.LastFourEps;

    /// <summary>
    /// Represents the weight of an object.
    /// </summary>
    public decimal Weight { get; set; } = se.Weight;

    public int IndustryId { get; set; } = se.IndustryId;
    public int ExchangeMarketId { get; set; } = se.ExchangeMarketId;
}