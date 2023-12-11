namespace StockApi.Models.Entities
{
    public class StockEntity
    {
        public int ExchangeMarketId { get; set; }
        public int IndustryId { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        public string StockSymbol { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last one EPS (Earnings Per Share).
        /// </summary>
        public decimal LastOneEps { get; set; }

        /// <summary>
        /// Gets or sets the value of the last four earning per share (EPS).
        /// </summary>
        public decimal LastFourEps { get; set; }

        /// <summary>
        /// Represents the weight of an object.
        /// </summary>
        public decimal Weight { get; set; }
    }
}
