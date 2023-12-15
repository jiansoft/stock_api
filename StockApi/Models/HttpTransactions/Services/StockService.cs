using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Config;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

namespace StockApi.Models.HttpTransactions.Services;

public class StockService(StocksDataProvider sp, CacheDataProvider cp)
{
    /// <summary>
    /// Retrieves stock details response.
    /// </summary>
    /// <param name="request">The request object containing the necessary information to retrieve stock details.</param>
    /// <returns>A response object containing a list of stock information.</returns>
    public DetailsResponse GetDetailsResponse(DetailsRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new StocksParam(request);
            var result = sp.GetStocks(param);
            var data = result.Entities.Select(s => new DetailDto(s));

            return new DetailsResponse(result.Meta, data)
            {
                Code = StatusCodes.Status200OK,
            };
        });
    }

    /// <summary>
    /// 股票產業分類
    /// </summary>
    /// <param name="request">查詢參數</param>
    /// <returns>股票產業分類</returns>
    public IndustriesResponse GetIndustriesResponse(IndustriesRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)), () =>
        {
            var param = new IndustriesParam(request);
            var result = sp.GetIndustries(param);
            var data = result.Entities.Select(s => new IndustryDto(s));

            return new IndustriesResponse(data)
            {
                Code = StatusCodes.Status200OK
            };
        });
    }

    /// <summary>
    /// 股票歷年發放股利
    /// </summary>
    /// <param name="request">查詢參數</param>
    /// <returns>股票歷年發放股利</returns>
    public DividendResponse GetDividendResponse(DividendRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new DividendParam(request);
            var result = sp.GetDividend(param);
            var data = result.Entities.Select(s => new DividendDto(s));

            return new DividendResponse(data)
            {
                Code = StatusCodes.Status200OK
            };
        });
    }

    public LastDailyQuoteResponse GetLastDailyQuoteResponse(LastDailyQuoteRequest request)
    {
        var now = DateTime.Now;
        var next3Pm = new DateTime(now.Year, now.Month, now.Day, 15, 0, 0);
        // 如果當前時間已經超過當天的下午3點，則使用明天的下午3點
        if (now > next3Pm)
        {
            next3Pm = next3Pm.AddDays(1);
        }

        var timeDifference = next3Pm - now;

        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(timeDifference), () =>
        {
            var paramLastDailyQuote = new LastDailyQuoteParam(request);
            var resultDailyQuote = sp.GetLastDailyQuote(paramLastDailyQuote);
            var paramConfig = new ConfigParam(Constants.KeyLastClosingKay);
            var resultConfig = sp.GetConfig(paramConfig);
            var data = resultDailyQuote.Entities.Select(s => new LastDailyQuoteDto(s));
            var payload = new LastDailyQuotePayload(resultConfig.Entity.Val, data);

            return new LastDailyQuoteResponse(resultDailyQuote.Meta, payload)
            {
                Code = StatusCodes.Status200OK
            };
        });
    }
}