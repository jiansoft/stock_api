using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

namespace StockApi.Models.HttpTransactions.Services;

using DataProviders;
using DataProviders.Config;
using DataProviders.Stocks;
using Defines;
using Stock.Details;
using Stock.Dividend;
using Stock.Industry;
using Stock.LastDailyQuote;

public class StockService(StocksDataProvider sp, CacheDataProvider cp)
{
    /// <summary>
    /// Retrieves stock details response.
    /// </summary>
    /// <param name="req">The request object containing the necessary information to retrieve stock details.</param>
    /// <returns>A response object containing a list of stock information.</returns>
    public IResponse<IPagingPayload<DetailDto>> GetDetailsResponse(DetailsRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new StocksParam(req);
            var result = sp.GetStocks(param);
            var data = result.Entities.Select(s => new DetailDto(s));
            var payload = new DetailsPayload<DetailDto>(result.Meta, data);
            var response = new DetailsResponse<IPagingPayload<DetailDto>>(payload)
            {
                Code = StatusCodes.Status200OK,
            };

            return response;
        });
    }

    /// <summary>
    /// 股票產業分類
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <returns>股票產業分類</returns>
    public IResponse<IPayload<IEnumerable<IndustryDto>>> GetIndustriesResponse(IndustriesRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)), () =>
        {
            var param = new IndustriesParam(req);
            var result = sp.GetIndustries(param);
            var data = result.Entities.Select(s => new IndustryDto(s));
            var payload = new IndustriesPayload<IEnumerable<IndustryDto>>(data);
            var response = new IndustriesResponse<IPayload<IEnumerable<IndustryDto>>>(payload)
            {
                Code = StatusCodes.Status200OK
            };

            return response;
        });
    }

    /// <summary>
    /// 股票歷年發放股利
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <returns>股票歷年發放股利</returns>
    public IResponse<IPayload<IEnumerable<DividendDto>>> GetDividendResponse(DividendRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new DividendParam(req);
            var result = sp.GetDividend(param);
            var data = result.Entities.Select(s => new DividendDto(s));
            var payload = new DividendPayload<IEnumerable<DividendDto>>(data);
            var response = new DividendResponse<IPayload<IEnumerable<DividendDto>>>(payload)
            {
                Code = StatusCodes.Status200OK
            };

            return response;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <returns></returns>
    public IResponse<IPagingPayload<LastDailyQuoteDto>> GetLastDailyQuoteResponse(LastDailyQuoteRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var paramLastDailyQuote = new LastDailyQuoteParam(req);
                var quotes = sp.GetLastDailyQuote(paramLastDailyQuote);
                var paramConfig = new ConfigParam(Constants.KeyLastClosingKay);
                var config = sp.GetConfig(paramConfig);
                var data = quotes.Result.Select(s => new LastDailyQuoteDto(s));
                var payload = new LastDailyQuotePayload<LastDailyQuoteDto>(config.Entity.Val, quotes.Meta, data);
                var response = new LastDailyQuoteResponse<IPagingPayload<LastDailyQuoteDto>>(payload)
                {
                    Code = StatusCodes.Status200OK
                };

                return response;
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <returns></returns>
    public IResponse<IPagingPayload<HistoricalDailyQuoteDto>> GetHistoricalDailyQuoteResponse(
        HistoricalDailyQuoteRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new HistoricalDailyQuoteParam(req);
                var quotes = sp.GetHistoricalDailyQuote(param);
                var data = quotes.Result.Select(s => new HistoricalDailyQuoteDto(s));
                var payload = new HistoricalDailyQuotePayload<HistoricalDailyQuoteDto>(quotes.Meta, data);
                var response =
                    new HistoricalDailyQuoteResponse<IPagingPayload<HistoricalDailyQuoteDto>>(payload)
                    {
                        Code = StatusCodes.Status200OK
                    };

                return response;
            });
    }
}