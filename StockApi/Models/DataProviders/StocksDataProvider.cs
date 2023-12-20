using jIAnSoft.Brook.Mapper;
using StockApi.Models.Entities;
using System.Data;
using System.Text;

namespace StockApi.Models.DataProviders;

using Config;
using Stocks;
using HttpTransactions;

public class StocksDataProvider(CacheDataProvider cp) : DbDataProvider
{
    /// <summary>
    /// Retrieves the stock details based on the provided parameters.
    /// </summary>
    /// <param name="param">The parameters for retrieving the stock details.</param>
    /// <returns>The stock details.</returns>
    public StocksResult GetStocks(StocksParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromHours(1)), () =>
        {
            const string table = "stocks";
            const string where = "stock_exchange_market_id in (2, 5)";

            using var db = Brook.Load("stock");

            var recordCount = GetOne(db, table, where);
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
                    stock_exchange_market_id in (2, 4, 5)
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

    public IndustriesResult GetIndustries(IKey param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromHours(1)), () =>
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

    public DividendResult GetDividend(DividendParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromHours(1)), () =>
        {
            using var db = Brook.Load("stock");

            return new DividendResult(db.Query<DividendEntity>(
                """
                select
                    d.security_code as "StockSymbol",
                    d.year as "Year",
                    year_of_dividend as "YearOfDividend",
                    d.quarter as "Quarter",
                    cash_dividend as "CashDividend",
                    stock_dividend as "StockDividend",
                    sum as "Sum",
                    COALESCE (fs.earnings_per_share,0) AS "EarningsPerShare",
                    "ex-dividend_date1" as "ExDividendDate1",
                    "ex-dividend_date2" as "ExDividendDate2",
                    payable_date1 as "PayableDate1",
                    payable_date2 as "PayableDate2",
                    payout_ratio as "PayoutRatio",
                    cash_dividend/ dq."ClosingPrice" * 100 as "CashDividendYield",
                    dq."Date" AS previous_trading_day,
                    dq."ClosingPrice" AS closing_price_on_previous_day
                from dividend as d
                left join financial_statement AS fs ON d.security_code = fs.security_code and d.year_of_dividend = fs.year AND d.quarter = fs.quarter
                left join LATERAL (
                    select
                        "Date",
                        "ClosingPrice"
                    from
                        "DailyQuotes" dq
                    where
                        dq."SecurityCode" = d.security_code
                		AND d."ex-dividend_date1" != '-' AND d."ex-dividend_date1" != '尚未公布'
                        AND dq."Date" < TO_DATE(d."ex-dividend_date1", 'YYYY-MM-DD')
                    order by
                        "Date" desc
                    limit 1
                ) dq ON TRUE
                where d.security_code = @security_code
                order by d.year desc, year_of_dividend desc, d.quarter desc
                """, new[]
                {
                    db.Parameter("@security_code", param.StockSymbol)
                }));
        });

        return result;
    }

    public LastDailyQuoteResult<IEnumerable<LastDailyQuoteEntity>> GetLastDailyQuote(LastDailyQuoteParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromMinutes(1)), () =>
        {
            const string table = "stock_exchange_market as sem";
            const string join = """
                                inner join stocks as s on s.stock_exchange_market_id = sem.stock_exchange_market_id
                                inner join last_daily_quotes as ldq on s.stock_symbol = ldq.security_code
                                """;
            const string where = "s.\"SuspendListing\" = false";

            using var db = Brook.Load("stock");

            var recordCount = GetOne(db, table, where, join);
            var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

            if (recordCount == 0)
            {
                return new LastDailyQuoteResult<IEnumerable<LastDailyQuoteEntity>>(meta, []);
            }

            var sb = new StringBuilder();
            sb.Append(
                """
                select
                    sem.stock_exchange_market_id as "StockExchangeMarketId",
                    s.stock_symbol as "StockSymbol",
                    trading_volume as "TradingVolume",
                    "transaction" as "Transaction",
                    trade_value as "TradeValue",
                    opening_price as "OpeningPrice",
                    highest_price as "HighestPrice",
                    lowest_price as "LowestPrice",
                    closing_price as "ClosingPrice",
                    change_range as "ChangeRange",
                    change as "Change",
                    moving_average_5 as "MovingAverage5",
                    moving_average_10 as "MovingAverage10",
                    moving_average_20 as "MovingAverage20",
                    moving_average_60 as "MovingAverage60",
                    moving_average_120 as "MovingAverage120",
                    moving_average_240 as "MovingAverage240",
                    maximum_price_in_year as "MaximumPriceInYear",
                    minimum_price_in_year as "MinimumPriceInYear",
                    average_price_in_year as "AveragePriceInYear",
                    maximum_price_in_year_date_on as "MaximumPriceInYearDateOn",
                    minimum_price_in_year_date_on as "MinimumPriceInYearDateOn",
                    "price-to-book_ratio" as "PriceToBookRatio"
                from stock_exchange_market as sem
                inner join stocks as s on s.stock_exchange_market_id = sem.stock_exchange_market_id
                inner join last_daily_quotes as ldq on s.stock_symbol = ldq.security_code
                where s."SuspendListing" = false
                order by s.stock_symbol
                offset @pi limit @ps;
                """);


            return new LastDailyQuoteResult<IEnumerable<LastDailyQuoteEntity>>(meta, db.Query<LastDailyQuoteEntity>(
                sb.ToString(), new[]
                {
                    db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                    db.Parameter("@ps", param.PageSize, DbType.Int64)
                }));
        });

        return result;
    }

    public ConfigResult GetConfig(ConfigParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                using var db = Brook.Load("stock");

                return new ConfigResult(db.First<ConfigEntity>(
                    """
                    select
                        key as "Key", val as "Val"
                    from
                        config
                    where key = @key;
                    """, new[]
                    {
                        db.Parameter("@key", param.Key)
                    }));
            });

        return result;
    }
    
    //HistoricalDailyQuote
    
    public HistoricalDailyQuoteResult<IEnumerable<HistoricalDailyQuoteEntity>> GetHistoricalDailyQuote(HistoricalDailyQuoteParam param)
    {
        var result = cp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromMinutes(1)), () =>
        {
            const string table = "stock_exchange_market as sem";
            const string join = """
                                inner join stocks as s on s.stock_exchange_market_id = sem.stock_exchange_market_id
                                inner join "DailyQuotes" as dq on s.stock_symbol = dq."SecurityCode"
                                """;
             var where = $"""dq."Date" = '{param.Date.ToString("yyyy-MM-dd")}' and s."SuspendListing" = false""";

            using var db = Brook.Load("stock");

            var recordCount = GetOne(db, table, where, join);
            var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

            if (recordCount == 0)
            {
                return new HistoricalDailyQuoteResult<IEnumerable<HistoricalDailyQuoteEntity>>(meta, []);
            }

            var sb = new StringBuilder();
            sb.Append(
                $"""
                 select
                    sem.stock_exchange_market_id as "StockExchangeMarketId",
                    s.stock_symbol as "StockSymbol",
                    "TradingVolume",
                    "Transaction",
                    "TradeValue",
                    "OpeningPrice",
                    "HighestPrice",
                    "LowestPrice",
                    "ClosingPrice",
                    "ChangeRange",
                    "Change",
                    "MovingAverage5",
                    "MovingAverage10",
                    "MovingAverage20",
                    "MovingAverage60",
                    "MovingAverage120",
                    "MovingAverage240",
                    maximum_price_in_year as "MaximumPriceInYear",
                    minimum_price_in_year as "MinimumPriceInYear",
                    average_price_in_year as "AveragePriceInYear",
                    maximum_price_in_year_date_on as "MaximumPriceInYearDateOn",
                    minimum_price_in_year_date_on as "MinimumPriceInYearDateOn",
                    "price-to-book_ratio" as "PriceToBookRatio"
                 from stock_exchange_market as sem
                 inner join stocks as s on s.stock_exchange_market_id = sem.stock_exchange_market_id
                 inner join "DailyQuotes" as dq on s.stock_symbol = dq."SecurityCode"
                 where dq."Date" = @date and s."SuspendListing" = false
                 order by s.stock_symbol
                 offset @pi limit @ps;
                 """);


            return new HistoricalDailyQuoteResult<IEnumerable<HistoricalDailyQuoteEntity>>(meta, db.Query<HistoricalDailyQuoteEntity>(
                sb.ToString(), new[]
                {
                    db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                    db.Parameter("@ps", param.PageSize, DbType.Int64),
                    db.Parameter("@date", param.Date, DbType.Date)
                }));
        });

        return result;
    }
}

