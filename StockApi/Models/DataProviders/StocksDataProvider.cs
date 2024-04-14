using jIAnSoft.Brook.Mapper;
using Microsoft.EntityFrameworkCore;
using StockApi.Models.Entities;
using System.Data;
using System.Text;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.HttpTransactions;
using System.Data.Common;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 股票資料提供者
/// </summary>
/// <param name="cdp"></param>
public class StocksDataProvider(CacheDataProvider cdp) : DbDataProvider
{
    internal DividendResult GetDividend(DividendParam param, StockContext sc)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromHours(1)), () =>
        {
            var qq = sc.Database.SqlQuery<DividendEntity>(
                $"""
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
                     COALESCE (cash_dividend/ dq."ClosingPrice" * 100,0) as "CashDividendYield",
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
                 where d.security_code = {param.StockSymbol}
                 order by d.year desc, year_of_dividend desc, d.quarter desc
                 """).ToList();

            return new DividendResult(qq);
        });

        return result;
    }

    internal LastDailyQuoteResult GetLastDailyQuote(LastDailyQuoteParam param)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromMinutes(1)), () =>
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
                return new LastDailyQuoteResult(meta, []);
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


            return new LastDailyQuoteResult(meta, db.Query<LastDailyQuoteEntity>(
                sb.ToString(), [
                    db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                    db.Parameter("@ps", param.PageSize, DbType.Int64)
                ]));
        });

        return result;
    }

    internal ConfigResult GetConfig(ConfigParam param, StockContext sc)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () => { return new ConfigResult(sc.Configs.Single(w => w.Key == param.Key)); });

        return result;
    }

    internal HistoricalDailyQuoteResult GetHistoricalDailyQuote(HistoricalDailyQuoteParam param)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromMinutes(1)), () =>
        {
            const string table = "stock_exchange_market as sem";
            const string join = """
                                inner join stocks as s on s.stock_exchange_market_id = sem.stock_exchange_market_id
                                inner join "DailyQuotes" as dq on s.stock_symbol = dq."SecurityCode"
                                """;
            var where = $"""dq."Date" = '{param.Date:yyyy-MM-dd}' and s."SuspendListing" = false""";

            using var db = Brook.Load("stock");

            var recordCount = GetOne(db, table, where, join);
            var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

            if (recordCount == 0)
            {
                return new HistoricalDailyQuoteResult(meta, []);
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


            return new HistoricalDailyQuoteResult(meta,
                db.Query<HistoricalDailyQuoteEntity>(
                    sb.ToString(), [
                        db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                        db.Parameter("@ps", param.PageSize, DbType.Int64),
                        db.Parameter("@date", param.Date, DbType.Date)
                    ]));
        });

        return result;
    }

    internal IndexResult GetIndex(IndexParam param)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                const string table = "index";
                var where = $"category  = '{param.Category}'";

                using var db = Brook.Load("stock");

                var recordCount = GetOne(db, table, where);
                var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

                if (recordCount == 0)
                {
                    return new IndexResult(meta, []);
                }


                return new IndexResult(meta, db.Query<IndexEntity>(
                    """
                    select
                        category as "Category",
                        date as "Date",
                        trading_volume as "TradingVolume",
                        transaction as "Transaction",
                        trade_value as "TradeValue",
                        change as "Change",
                        index as "Index"
                    from
                        index
                    where category = @category
                    order by date desc
                    offset @pi limit @ps;
                    """, [
                        db.Parameter("@pi", (param.PageIndex - 1) * param.PageSize, DbType.Int64),
                        db.Parameter("@ps", param.PageSize, DbType.Int64),
                        db.Parameter("@category", param.Category)
                    ]));
            });

        return result;
    }

    internal RevenueResult<IEnumerable<RevenueEntity>> GetRevenue(RevenueParam param)
    {
        var result = cdp.GetOrSet(param.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                using var db = Brook.Load("stock");
                const string table = "\"Revenue\"";
                var where = "where 1=1 ";
                var queryCountParams = new List<DbParameter>();
                var queryParams = new List<DbParameter>();
                if (!string.IsNullOrEmpty(param.StockSymbol) && !string.IsNullOrWhiteSpace(param.StockSymbol))
                {

                    where += "and \"SecurityCode\" = @sc ";
                    queryParams.Add(db.Parameter("@sc", param.StockSymbol));
                    queryCountParams.Add(db.Parameter("@sc", param.StockSymbol));

                }

                if (param.MonthOfYear != 0)
                {
                    where += "and \"Date\" = @d ";
                    queryParams.Add(db.Parameter("@d", param.MonthOfYear, DbType.Int64));
                    queryCountParams.Add(db.Parameter("@d", param.MonthOfYear, DbType.Int64));
                }

                var recordCount = GetOne(db, table, "", where, queryCountParams.ToArray());
                var meta = new Meta(recordCount, param.PageIndex, param.PageSize);

                if (recordCount == 0)
                {
                    return new RevenueResult<IEnumerable<RevenueEntity>>(meta, []);
                }

                queryParams.AddRange([
                    db.Parameter("@pi", meta.Offset, DbType.Int64),
                    db.Parameter("@ps", param.PageSize, DbType.Int64),
                ]);

                return new RevenueResult<IEnumerable<RevenueEntity>>(meta, db.Query<RevenueEntity>(
                    """
                     select
                         "SecurityCode" as "StockSymbol",
                         "Date",
                         "Monthly",
                         "LastMonth",
                         "LastYearThisMonth",
                         "MonthlyAccumulated",
                         "LastYearMonthlyAccumulated",
                         "ComparedWithLastMonth",
                         "ComparedWithLastYearSameMonth",
                         "AccumulatedComparedWithLastYear",
                         avg_price as "AvgPrice",
                         lowest_price as "LowestPrice",
                         highest_price as "HighestPrice"
                     from
                        "Revenue"
                    """ + where + """
                                  order by "Date" desc, "SecurityCode"
                                  offset @pi limit @ps;
                                  """,
                    queryParams.ToArray()));
            });

        return result;
    }
}

