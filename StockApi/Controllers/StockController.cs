using Microsoft.AspNetCore.Mvc;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Services;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Revenue;

namespace StockApi.Controllers;

/// <summary>
/// 控製器用於處理與股票相關的HTTP請求。提供了獲取股票基本資訊、股票行業分類、分紅資訊、最近的收盤價、歷史收盤價及收入資料的方法。
/// </summary>
/// <param name="ss">提供股票相關服務的服務類實例。</param>
[Route("api/stock")]
[ApiController]
public class StockController(StockService ss) : ControllerBase
{
    /// <summary>
    /// 獲取股票基本資料
    /// </summary>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns>股票基本資料</returns>
    [HttpGet]
    [Route("details")]
    [ProducesResponseType<IResponse<IPagingPayload<DetailDto>>>(StatusCodes.Status200OK)]
    public IActionResult Details(int? requestedPage, int? recordsPerPage)
    {
        var request = new DetailsRequest(requestedPage, recordsPerPage);

        return Ok(ss.GetDetailsResponse(request));
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("industry")]
    [ProducesResponseType<IResponse<IPagingPayload<IndustryDto>>>(StatusCodes.Status200OK)]
    public IActionResult Industries()
    {
        return Ok(ss.GetIndustriesResponse(new IndustriesRequest()));
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("dividend/{stockSymbol}")]
    [ProducesResponseType<IResponse<IPagingPayload<DividendDto>>>(StatusCodes.Status200OK)]
    public IActionResult Dividend(string stockSymbol)
    {
        var request = new DividendRequest(stockSymbol);

        return Ok(ss.GetDividendResponse(request));
    }

    /// <summary>
    /// 取得最後的收盤股價
    /// </summary>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("last_daily_quote")]
    [ProducesResponseType<IResponse<IPagingPayload<LastDailyQuoteDto>>>(StatusCodes.Status200OK)]
    public IActionResult LastDailyQuote(int? requestedPage, int? recordsPerPage)
    {
        var request = new LastDailyQuoteRequest(requestedPage, recordsPerPage);

        return Ok(ss.GetLastDailyQuoteResponse(request));
    }

    /// <summary>
    /// 取得歷史的收盤股價
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("historical_daily_quote/{date}")]
    [ProducesResponseType<IResponse<IPagingPayload<HistoricalDailyQuoteDto>>>(StatusCodes.Status200OK)]
    public IActionResult HistoricalDailyQuote(DateOnly date, int? requestedPage, int? recordsPerPage)
    {
        var request = new HistoricalDailyQuoteRequest(requestedPage, recordsPerPage)
        {
            Date = date
        };

        return Ok(ss.GetHistoricalDailyQuoteResponse(request));
    }

    /// <summary>
    /// 取得歷史的收盤股價
    /// </summary>
    /// <param name="monthOfYear">日期，格式︰yyyyMM</param>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("revenue_on/{monthOfYear:int}")]
    [ProducesResponseType<IResponse<IPagingPayload<RevenueDto>>>(StatusCodes.Status200OK)]
    public IActionResult Revenue(int monthOfYear, int? requestedPage, int? recordsPerPage)
    {
        var request = new RevenueRequest(requestedPage, recordsPerPage)
        {
            MonthOfYear = monthOfYear
        };

        return Ok(ss.GetRevenueResponse(request));
    }

    /// <summary>
    /// 取得歷史的收盤股價
    /// </summary>
    /// <param name="stockSymbol">指定股票</param>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("revenue_by/{stockSymbol}")]
    [ProducesResponseType<IResponse<IPagingPayload<RevenueDto>>>(StatusCodes.Status200OK)]
    public IActionResult Revenue(string stockSymbol, int? requestedPage, int? recordsPerPage)
    {
        var request = new RevenueRequest(requestedPage, recordsPerPage)
        {
            StockSymbol = stockSymbol
        };

        return Ok(ss.GetRevenueResponse(request));
    }
}