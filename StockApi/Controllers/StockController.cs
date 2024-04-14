using Microsoft.AspNetCore.Mvc;
using StockApi.Models.DataProviders;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Revenue;
using StockApi.Services;

namespace StockApi.Controllers;

/// <summary>
/// 控製器用於處理與股票相關的HTTP請求。提供了獲取股票基本資訊、股票行業分類、分紅資訊、最近的收盤價、歷史收盤價及收入資料的方法。
/// </summary>
/// <param name="ss">提供股票相關服務的服務類實例。</param>
/// <param name="sc"></param>
[Route("api/stock")]
[ApiController]
[Produces("application/json")]
public class StockController(StockService ss, StockContext sc) : ControllerBase
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
    public async Task<IActionResult> Details(uint? requestedPage, int? recordsPerPage)
    {
        var request = new DetailsRequest(requestedPage, recordsPerPage);
        var response = await ss.GetDetailsAsync(request, sc);

        return Ok(response);
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("industry")]
    [ProducesResponseType<IResponse<IPayload<IEnumerable<IndustryDto>>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Industries()
    {
        var request = new IndustriesRequest();
        var response = await ss.GetIndustriesAsync(request, sc);

        return Ok(response);
    }

    /// <summary>
    /// 取得股票歷年發放股利
    /// </summary>
    /// <returns>歷年發放股利</returns>
    [HttpGet]
    [Route("dividend/{stockSymbol}")]
    [ProducesResponseType<IResponse<IPayload<IEnumerable<DividendDto>>>>(StatusCodes.Status200OK)]
    public IActionResult Dividend(string stockSymbol)
    {
        var request = new DividendRequest(stockSymbol);
        var response = ss.GetDividendResponse(request, sc);
        
        return Ok(response);
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
    public IActionResult LastDailyQuote(uint? requestedPage, int? recordsPerPage)
    {
        var request = new LastDailyQuoteRequest(requestedPage, recordsPerPage);
        var response = ss.GetLastDailyQuoteResponse(request, sc);

        return Ok(response);
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
    public IActionResult HistoricalDailyQuote(DateOnly date, uint? requestedPage, int? recordsPerPage)
    {
        var request = new HistoricalDailyQuoteRequest(requestedPage, recordsPerPage)
        {
            Date = date
        };
        var response = ss.GetHistoricalDailyQuoteResponse(request);
        
        return Ok(response);
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
    public IActionResult Revenue(int monthOfYear, uint? requestedPage, int? recordsPerPage)
    {
        var request = new RevenueRequest(requestedPage, recordsPerPage)
        {
            MonthOfYear = monthOfYear
        };
        var response = ss.GetRevenueResponse(request);

        return Ok(response);
    }

    /// <summary>
    /// 取得營收相關數據
    /// </summary>
    /// <param name="stockSymbol">指定股票</param>
    /// <param name="requestedPage">取得第幾頁的數據</param>
    /// <param name="recordsPerPage">每頁幾筆數據</param>
    /// <returns></returns>
    [HttpGet]
    [Route("revenue_by/{stockSymbol}")]
    [ProducesResponseType<IResponse<IPagingPayload<RevenueDto>>>(StatusCodes.Status200OK)]
    public IActionResult Revenue(string stockSymbol, uint? requestedPage, int? recordsPerPage)
    {
        var request = new RevenueRequest(requestedPage, recordsPerPage)
        {
            StockSymbol = stockSymbol
        };
        var response = ss.GetRevenueResponse(request);
        
        return Ok(response);
    }

    /// <summary>
    /// 獲取指定年份的休市日期
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("holiday_schedule/{year:int}")]
    [ProducesResponseType<IResponse<IPayload<IEnumerable<HolidayScheduleDto>>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> HolidaySchedule(int year)
    {
        var request = new HolidayScheduleRequest(year);
        var response = await ss.GetHolidayScheduleResponseAsync(request);

        return Ok(response);
    }
}