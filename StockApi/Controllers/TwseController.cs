using Microsoft.AspNetCore.Mvc;
using StockApi.Models.Defines;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Services;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Controllers;

[Route("api/twse")]
[ApiController]
public class TwseController(TwseService ts) : ControllerBase
{
    [HttpGet]
    [Route("taiex")]
    public IResponse<IPagingPayload<TaiexDto>> Taiex(int? pageIndex, int? pageSize)
    {
        var request = new TaiexRequest
        {
            PageIndex = pageIndex is null or <= 0
                ? Constants.DefaultPageIndex
                : pageIndex.Value,
            PageSize = pageSize is null or <= 0 or > Constants.MaximumPageSize
                ? Constants.DefaultPageSize
                : pageSize.Value
        };

        return ts.GetTaiexResponse(request);
    }
}