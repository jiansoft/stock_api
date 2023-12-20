using Microsoft.AspNetCore.Mvc;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Services;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

namespace StockApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(StockService ss) : ControllerBase
{
    /// <summary>
    /// 獲取股票基本資料
    /// </summary>
    /// <param name="pageIndex">取得第幾頁的數據</param>
    /// <param name="pageSize">每頁幾筆數據</param>
    /// <returns>股票基本資料</returns>
    [HttpGet]
    [Route("details")]
    public IResponse<IPagingPayload<DetailDto>> Details(int? pageIndex, int? pageSize)
    {
        var request = new DetailsRequest
        {
            PageIndex = pageIndex is null or <= 0
                ? Constants.DefaultPageIndex
                : pageIndex.Value,
            PageSize = pageSize is null or <= 0 or > Constants.MaximumPageSize
                ? Constants.DefaultPageSize
                : pageSize.Value
        };

        return ss.GetDetailsResponse(request);
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("industry")]
    public IResponse<IPayload<IEnumerable<IndustryDto>>> Industries()
    {
        return ss.GetIndustriesResponse(new IndustriesRequest());
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("dividend/{stockSymbol}")]
    public IResponse<IPayload<IEnumerable<DividendDto>>> Dividend(string stockSymbol)
    {
        var request = new DividendRequest(stockSymbol);

        return ss.GetDividendResponse(request);
    }

    /// <summary>
    /// 取得最近的收盤股價
    /// </summary>
    /// <param name="pageIndex">取得第幾頁的數據</param>
    /// <param name="pageSize">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("last_daily_quote")]
    public IResponse<IPagingPayload<LastDailyQuoteDto>> LastDailyQuote(int? pageIndex, int? pageSize)
    {
        var request = new LastDailyQuoteRequest
        {
            PageIndex = pageIndex is null or <= 0
                ? Constants.DefaultPageIndex
                : pageIndex.Value,
            PageSize = pageSize is null or <= 0 or > Constants.MaximumPageSize
                ? Constants.DefaultPageSize
                : pageSize.Value
        };

        return ss.GetLastDailyQuoteResponse(request);
    }

    /// <summary>
    /// 取得歷史的收盤股價
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="pageIndex">取得第幾頁的數據</param>
    /// <param name="pageSize">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("historical_daily_quote/{date}")]
    public ActionResult<IResponse<IPagingPayload<HistoricalDailyQuoteDto>>> HistoricalDailyQuote(
        DateOnly date,
        int? pageIndex,
        int? pageSize)
    {
        var request = new HistoricalDailyQuoteRequest
        {
            PageIndex = pageIndex is null or <= 0
                ? Constants.DefaultPageIndex
                : pageIndex.Value,
            PageSize = pageSize is null or <= 0 or > Constants.MaximumPageSize
                ? Constants.DefaultPageSize
                : pageSize.Value,
            Date = date
        };

        return Ok(ss.GetHistoricalDailyQuoteResponse(request));
    }
}