using AutoMapper;
using Stock;
using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Revenue;


namespace StockApi.Services;

/// <summary>
/// 股票服務類，負責處理股票相關的數據查詢和處理。
/// </summary>
/// <param name="sdp">股票數據提供者，用於從數據源獲取股票數據。</param>
/// <param name="cdp">緩存數據提供者，用於緩存和檢索股票數據。</param>
/// <param name="mapper">物件對應器，用於在不同的數據模型之間進行轉換。</param>
/// <param name="gs">gRPC提供者</param>
public class StockService(StocksDataProvider sdp, CacheDataProvider cdp, IMapper mapper, GrpcService gs)
{
    /// <summary>
    /// Retrieves stock details response.
    /// </summary>
    /// <param name="req">The request object containing the necessary information to retrieve stock details.</param>
    /// <returns>A response object containing a list of stock information.</returns>
    internal IHttpTransaction GetDetailsResponse(DetailsRequest req)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new StocksParam(req);
            var result = sdp.GetStocks(param);
            var data = mapper.Map<IEnumerable<DetailDto>>(result.Result);
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
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)), () =>
        {
            var param = new IndustriesParam(req);
            var result = sdp.GetIndustries(param);
            var data = mapper.Map<IEnumerable<IndustryDto>>(result.Entities);
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
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new DividendParam(req);
            var result = sdp.GetDividend(param);
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
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var paramLastDailyQuote = new LastDailyQuoteParam(req);
                var result = sdp.GetLastDailyQuote(paramLastDailyQuote);
                var paramConfig = new ConfigParam(Constants.KeyLastClosingKay);
                var config = sdp.GetConfig(paramConfig);
                var data = mapper.Map<IEnumerable<LastDailyQuoteDto>>(result.Result);
                var payload = new LastDailyQuotePayload<LastDailyQuoteDto>(config.Entity.Val, result.Meta, data);
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
    internal IHttpTransaction GetHistoricalDailyQuoteResponse(HistoricalDailyQuoteRequest req)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new HistoricalDailyQuoteParam(req);
                var result = sdp.GetHistoricalDailyQuote(param);
                var data = mapper.Map<IEnumerable<HistoricalDailyQuoteDto>>(result.Result);
                var payload = new PagingPayload<HistoricalDailyQuoteDto>(result.Meta, data);
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
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new RevenueParam(req);
                var result = sdp.GetRevenue(param);
                var data = mapper.Map<IEnumerable<RevenueDto>>(result.Result);
                var payload = new PagingPayload<RevenueDto>(result.Meta, data);
                var response =
                    new RevenueResponse<IPagingPayload<RevenueDto>>(payload)
                    {
                        Code = StatusCodes.Status200OK
                    };

                return response;
            });
    }

    /// <summary>
    /// 獲取指定年份的休市日期
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal async Task<IHttpTransaction> GetHolidayScheduleResponseAsync(
        Models.HttpTransactions.Stock.HolidaySchedule.HolidayScheduleRequest req)
    {
        return await cdp.GetOrSetAsync(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            async () =>
            {
                var result = await gs.FetchHolidayScheduleAsync(req.Year);
                var data = mapper.Map<IEnumerable<HolidayScheduleDto>>(result);
                var payload = new HolidaySchedulePayload<IEnumerable<HolidayScheduleDto>>(data);
                var response =
                    new HolidayScheduleResponse<IPayload<IEnumerable<HolidayScheduleDto>>>(payload)
                    {
                        Code = StatusCodes.Status200OK
                    };
                return response;
            });
    }
}