using Microsoft.AspNetCore.Mvc;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions.Services;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
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
    public DetailsResponse Details(int? pageIndex, int? pageSize)
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
    public IndustriesResponse Industries()
    {
        return ss.GetIndustriesResponse(new IndustriesRequest());
    }

    /// <summary>
    /// 取得股票產業分類
    /// </summary>
    /// <returns>股票產業分類</returns>
    [HttpGet]
    [Route("dividend/{stockSymbol}")]
    public DividendResponse Dividend(string stockSymbol)
    {
        var request = new DividendRequest(stockSymbol);
        
        return ss.GetDividendResponse(request);
    }

    /// <summary>
    /// 取得最後收盤時的股價
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("last_daily_quote")]
    public LastDailyQuoteResponse LastDailyQuote(int? pageIndex, int? pageSize)
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
}