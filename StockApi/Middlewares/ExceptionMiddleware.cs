using Newtonsoft.Json;
using StockApi.Models.Exceptions;
using StockApi.Models.HttpTransactions;

namespace StockApi.Middlewares;

/// <summary>
/// 中介軟體，用於處理在請求管道中發生的異常。
/// </summary>
/// <param name="next">請求委託，表示管道中的下一個中介軟體</param>
/// <param name="logger">用於記錄異常資訊的日誌記錄器。</param>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private const string ContentType = "application/json";
    private const int ErrorResponseStatusCode = StatusCodes.Status500InternalServerError;

    /// <summary>
    /// 非同步呼叫中介軟體處理程序，處理HTTP上下文的請求和響應。
    /// 此方法嘗試執行下一個中介軟體，並捕獲過程中發生的任何異常。
    /// 針對已知異常類型提供自訂錯誤處理，對於其它異常提供通用處理。
    /// </summary>
    /// <param name="httpContext">當前請求的HTTP上下文。</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (MessageException ex)
        {
            LogException(ex);
            await HandleApiExceptionAsync(httpContext, ex.Message);
        }
        catch (Exception ex)
        {
            LogException(ex);
            if (IsApiRequest(httpContext))
            {
                await HandleApiExceptionAsync(httpContext, "Internal Server Error. Please try again later.");
            }
            else
            {
                HandleNonApiException(httpContext);
            }
        }
    }

    private void LogException(Exception ex)
    {
        logger.LogError(ex, "{Message}", ex.Message);
    }
    
    private static bool IsApiRequest(HttpContext httpContext)
    {
        return httpContext.Request.Path.StartsWithSegments("/api");
    }

    private static Task HandleApiExceptionAsync(HttpContext context,string message)
    {
        context.Response.ContentType = ContentType;
        context.Response.StatusCode = ErrorResponseStatusCode;

        var res = new ErrorResponse<string>(message)
            { Code = context.Response.StatusCode };
        var json = JsonConvert.SerializeObject(res);

        return context.Response.WriteAsync(json);
    }

    private static void HandleNonApiException(HttpContext context)
    {
        context.Response.Redirect("/Error"); // 將用戶重定向到錯誤頁面
    }
}