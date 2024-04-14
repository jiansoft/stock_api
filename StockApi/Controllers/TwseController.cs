using Microsoft.AspNetCore.Mvc;
using StockApi.Models.DataProviders;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Twse;
using StockApi.Services;

namespace StockApi.Controllers;

/// <summary>
/// 提供台灣證券交易所(TWSE)相關數據的API控制器。
/// </summary>
/// <param name="ts">提供台灣證券交易所數據的服務。</param>
/// <param name="sc">股票數據的上下文，用於訪問數據庫。</param>
[Route("api/twse")]
[ApiController]
[Produces("application/json")]
public class TwseController(TwseService ts, StockContext sc) : ControllerBase
{
    /// <summary>
    /// 獲取台灣加權股價指數（TAIEX）的數據。
    /// </summary>
    /// <param name="requestedPage">請求的頁碼，指定要獲取第幾頁的數據。</param>
    /// <param name="recordsPerPage">每頁的記錄數量，指定每頁顯示多少條數據。</param>
    /// <returns>包含台灣加權股價指數數據的回應。</returns>
    [HttpGet]
    [Route("taiex")]
    [ProducesResponseType<IResponse<IPagingPayload<TaiexDto>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Taiex(uint? requestedPage, int? recordsPerPage)
    {
        var request = new TaiexRequest(requestedPage, recordsPerPage);
        var response = await ts.GetTaiexAsync(request, sc);

        return Ok(response);
    }
}