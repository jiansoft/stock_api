﻿using Microsoft.AspNetCore.Mvc;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Services;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Controllers;

/// <summary>
/// 台灣證券交易所（TWSE）相關數據的控制器，負責處理與台灣證券交易所相關的HTTP請求。
/// </summary>
/// <param name="ts">台灣證券交易所服務的實例，用於處理相關請求。</param>
[Route("api/twse")]
[ApiController]
public class TwseController(TwseService ts) : ControllerBase
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
    public IActionResult Taiex(int? requestedPage, int? recordsPerPage)
    {
        var request = new TaiexRequest
            (requestedPage, recordsPerPage);

        return Ok(ts.GetTaiexResponse(request));
    }
}