using Newtonsoft.Json;
using StockApi.Models.Exceptions;
using StockApi.Models.HttpTransactions;

namespace StockApi.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private const string ContentType = "application/json";
    private const int ErrorResponseStatusCode = StatusCodes.Status500InternalServerError;

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

        var res = new ErrorResponse
            { Code = context.Response.StatusCode, Message = message };
        var json = JsonConvert.SerializeObject(res);

        return context.Response.WriteAsync(json);
    }

    private static void HandleNonApiException(HttpContext context)
    {
        context.Response.Redirect("/Error"); // 將用戶重定向到錯誤頁面
    }
}