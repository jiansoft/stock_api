using AutoMapper;
using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Revenue;

namespace StockApi.Models.HttpTransactions.Services;

/// <summary>
/// 股票服務類，負責處理股票相關的數據查詢和處理。
/// </summary>
/// <param name="sp">股票數據提供者，用於從數據源獲取股票數據。</param>
/// <param name="cp">緩存數據提供者，用於緩存和檢索股票數據。</param>
/// <param name="mapper">物件對應器，用於在不同的數據模型之間進行轉換。</param>
public class StockService(StocksDataProvider sp, CacheDataProvider cp, IMapper mapper)
{
    /// <summary>
    /// Retrieves stock details response.
    /// </summary>
    /// <param name="req">The request object containing the necessary information to retrieve stock details.</param>
    /// <returns>A response object containing a list of stock information.</returns>
    internal IHttpTransaction GetDetailsResponse(DetailsRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new StocksParam(req);
            var result = sp.GetStocks(param);
            var data = mapper.Map<IEnumerable<DetailDto>>(result.Entities);
            var payload = new PagingPayload<DetailDto>(result.Meta, data);
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
    internal IHttpTransaction GetIndustriesResponse(IndustriesRequest req)
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
    internal IHttpTransaction GetDividendResponse(DividendRequest req)
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
    /// 提供最後的每日報價數據。
    /// </summary>
    /// <param name="req">包含查詢條件的請求對象</param>
    /// <returns>包含每日報價數據的回應</returns>
    internal IHttpTransaction GetLastDailyQuoteResponse(LastDailyQuoteRequest req)
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
    /// 提供歷史每日報價數據
    /// </summary>
    /// <param name="req">包含查詢條件的請求對象</param>
    /// <returns>包含歷史每日報價數據的回應</returns>
    internal IHttpTransaction GetHistoricalDailyQuoteResponse(
        HistoricalDailyQuoteRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new HistoricalDailyQuoteParam(req);
                var quotes = sp.GetHistoricalDailyQuote(param);
                var data = quotes.Result.Select(s => new HistoricalDailyQuoteDto(s));
                var payload = new PagingPayload<HistoricalDailyQuoteDto>(quotes.Meta, data);
                var response =
                    new HistoricalDailyQuoteResponse<IPagingPayload<HistoricalDailyQuoteDto>>(payload)
                    {
                        Code = StatusCodes.Status200OK
                    };

                return response;
            });
    }

    /// <summary>
    /// 提供營收相關數據
    /// </summary>
    /// <param name="req">包含查詢條件的請求對象</param>
    /// <returns>包含收入數據的回應</returns>
    public IHttpTransaction GetRevenueResponse(RevenueRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new RevenueParam(req);
                var quotes = sp.GetRevenue(param);
                var data = quotes.Result.Select(s => new RevenueDto(s));
                var payload = new PagingPayload<RevenueDto>(quotes.Meta, data);
                var response =
                    new RevenueResponse<IPagingPayload<RevenueDto>>(payload)
                    {
                        Code = StatusCodes.Status200OK
                    };

                return response;
            });
    }
}