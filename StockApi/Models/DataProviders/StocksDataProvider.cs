using jIAnSoft.Brook.Mapper;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;
using System.Data;

namespace StockApi.Models.DataProviders;

public class StocksDataProvider(CacheDataProvider cp) : DbDataProvider
{
    /// <summary>
    /// Retrieves the stock details based on the provided parameters.
    /// </summary>
    /// <param name="param">The parameters for retrieving the stock details.</param>
    /// <returns>The stock details.</returns>
    public StocksResult GetStocks(StocksParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            const string table = "stocks";
            const string where = "stock_exchange_market_id in (2, 5)";

            using var db = Brook.Load("stock");
            
            var recordCount = GetOne(db, table,where);
            var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

            if (recordCount == 0)
            {
                return new StocksResult(meta, []);
            }
            
           
            var result = new StocksResult(meta, db.Query<StockEntity>(
                """
                select
                    "Name",
                    weight as "Weight",
                    last_one_eps as "LastOneEps",
                    last_four_eps as "LastFourEps",
                    stock_symbol as "StockSymbol",
                    stock_exchange_market_id as "ExchangeMarketId",
                    stock_industry_id as "IndustryId",
                    issued_share,
                    qfii_shares_held,
                    qfii_share_holding_percentage
                 from
                    stocks
                 where
                    stock_exchange_market_id in (2,5)
                 order by
                     stock_exchange_market_id,
                     stock_industry_id, stock_symbol
                offset @pi limit @ps
                """, new[]
                {
                    db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                    db.Parameter("@ps", param.PageSize, DbType.Int64)
                }));

            return result;
        });

        return result;
    }

    public IndustriesResult GetIndustries(IndustriesParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            using var db = Brook.Load("stock");

            return new IndustriesResult(db.Query<IndustryEntity>(
                """
                select
                    stock_industry_id as "IndustryId", name as "Name"
                from
                    stock_industry;
                """));
        });

        return result;
    }
}

