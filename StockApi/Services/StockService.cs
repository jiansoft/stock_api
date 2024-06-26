﻿using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Stock;
using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.Defines;
using StockApi.Models.Entities;
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
/// <param name="gs">gRPC提供者</param>
/// <param name="mapper">物件對應器，用於在不同的數據模型之間進行轉換。</param>
public class StockService(StocksDataProvider sdp, CacheDataProvider cdp, GrpcService gs, IMapper mapper)
{
    private static readonly int[] ExchangeMarketId = [2, 4, 5];

    internal async Task<IHttpTransaction> GetDetailsAsync(DetailsRequest req, StockContext sc)
    {
        return await cdp.GetOrSetAsync(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)),
            async () =>
            {
                var totalRecords = await sc.Stocks.LongCountAsync(w => ExchangeMarketId.Contains(w.ExchangeMarketId));
                var meta = new Meta(totalRecords, req.RequestedPage, req.RecordsPerPage);
                var result = await sc.Stocks
                    .Where(w => ExchangeMarketId.Contains(w.ExchangeMarketId))
                    .OrderBy(ob => ob.ExchangeMarketId)
                    .ThenBy(tb => tb.IndustryId)
                    .ThenBy(tb => tb.StockSymbol)
                    .Skip(meta.Offset)
                    .Take(meta.PageSize)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
                var data = mapper.Map<IEnumerable<StockEntity>, IEnumerable<DetailDto>>(result);
                var payload = new PagingPayload<DetailDto>(meta, data);

                return new DetailsResponse<IPagingPayload<DetailDto>>(StatusCodes.Status200OK, payload);
            });
    }

    /// <summary>
    /// 股票產業分類
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <param name="sc"></param>
    /// <returns>股票產業分類</returns>
    internal async Task<IHttpTransaction> GetIndustriesAsync(IndustriesRequest req, StockContext sc)
    {
        return await cdp.GetOrSetAsync(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)),
            async () =>
            {
                var result = await sc.Industries
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
                var data = mapper.Map<IEnumerable<StockIndustryEntity>, IEnumerable<IndustryDto>>(result);
                var payload = new IndustriesPayload<IEnumerable<IndustryDto>>(data);

                return new IndustriesResponse<IPayload<IEnumerable<IndustryDto>>>(StatusCodes.Status200OK, payload);
            });
    }

    /// <summary>
    /// 股票歷年發放股利
    /// </summary>
    /// <param name="req">查詢參數</param>
    /// <param name="sc"></param>
    /// <returns>股票歷年發放股利</returns>
    internal IHttpTransaction GetDividendResponse(DividendRequest req, StockContext sc)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new DividendParam(req);
            var result = sdp.GetDividend(param, sc);
            var data = mapper.Map<IEnumerable<DividendEntity>, IEnumerable<DividendDto>>(result.Entities);
            var payload = new DividendPayload<IEnumerable<DividendDto>>(data);

            return new DividendResponse<IPayload<IEnumerable<DividendDto>>>(StatusCodes.Status200OK, payload);
        });
    }

    /// <summary>
    /// 提供最後的每日報價數據。
    /// </summary>
    /// <param name="req">包含查詢條件的請求對象</param>
    /// <param name="sc"></param>
    /// <returns>包含每日報價數據的回應</returns>
    internal IHttpTransaction GetLastDailyQuoteResponse(LastDailyQuoteRequest req, StockContext sc)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var paramLastDailyQuote = new LastDailyQuoteParam(req);
                var result = sdp.GetLastDailyQuote(paramLastDailyQuote);
                var paramConfig = new ConfigParam(Constants.KeyLastClosing);
                var lastClose = sdp.GetConfig(paramConfig, sc);
                var data = mapper.Map<IEnumerable<LastDailyQuoteEntity>, IEnumerable<LastDailyQuoteDto>>(result.Rows);
                var payload = new LastDailyQuotePayload<LastDailyQuoteDto>(lastClose.Entity.Val, result.Meta, data);

                return new LastDailyQuoteResponse<IPagingPayload<LastDailyQuoteDto>>(payload)
                {
                    Code = StatusCodes.Status200OK
                };
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
                var r = sdp.GetHistoricalDailyQuote(param);
                var data = mapper.Map<IEnumerable<HistoricalDailyQuoteEntity>, IList<HistoricalDailyQuoteDto>>(r.Rows);
                var payload = new PagingPayload<HistoricalDailyQuoteDto>(r.Meta, data);

                return new HistoricalDailyQuoteResponse<IPagingPayload<HistoricalDailyQuoteDto>>(payload)
                {
                    Code = StatusCodes.Status200OK
                };
            });
    }

    /// <summary>
    /// 提供營收相關數據
    /// </summary>
    /// <param name="req">包含查詢條件的請求對象</param>
    /// <returns>包含收入數據的回應</returns>
    internal IHttpTransaction GetRevenueResponse(RevenueRequest req)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            () =>
            {
                var param = new RevenueParam(req);
                var result = sdp.GetRevenue(param);
                var data = mapper.Map<IEnumerable<RevenueEntity>, IEnumerable<RevenueDto>>(result.Rows);
                var payload = new PagingPayload<RevenueDto>(result.Meta, data);

                return new RevenueResponse<IPagingPayload<RevenueDto>>(payload)
                {
                    Code = StatusCodes.Status200OK
                };
            });
    }

    /// <summary>
    /// 獲取指定年份的休市日期
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    internal async Task<IHttpTransaction> GetHolidayScheduleResponseAsync(
        StockApi.Models.HttpTransactions.Stock.HolidaySchedule.HolidayScheduleRequest req)
    {
        return await cdp.GetOrSetAsync(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            async () =>
            {
                var result = await gs.FetchHolidayScheduleAsync(req.Year);
                var data = mapper.Map<IEnumerable<HolidaySchedule>, IEnumerable<HolidayScheduleDto>>(result);
                var payload = new HolidaySchedulePayload<IEnumerable<HolidayScheduleDto>>(data);
                
                return new HolidayScheduleResponse<IPayload<IEnumerable<HolidayScheduleDto>>>(payload)
                {
                    Code = StatusCodes.Status200OK
                };
            });
    }
}