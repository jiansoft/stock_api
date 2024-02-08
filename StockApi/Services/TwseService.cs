using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Services;

/// <summary>
/// 提供台灣證券交易所相關服務的類別，負責從數據來源獲取和處理台灣加權股價指數（TAIEX）等相關數據。
/// </summary>
/// <param name="sdp">股票數據提供者，用於從數據源獲取台灣證券交易所的數據。</param>
/// <param name="cdp">緩存數據提供者，用於緩存台灣證券交易所的數據，提高數據訪問效率。</param>
public class TwseService(StocksDataProvider sdp, CacheDataProvider cdp)
{
    /// <summary>
    /// 獲取台灣加權股價指數的回應。此方法從數據源中獲取台灣加權股價指數的數據，並透過緩存提高數據訪問效率。
    /// </summary>
    /// <param name="req">包含查詢條件的台灣加權股價指數請求對象。</param>
    /// <returns>包含台灣加權股價指數數據的回應對象。</returns>
    internal IHttpTransaction GetTaiexResponse(TaiexRequest req)
    {
        return cdp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)), () =>
        {
            var param = new IndexParam(req);
            var result = sdp.GetIndex(param);
            var data = result.Result.Select(s => new TaiexDto(s));
            var payload = new TaiexPayload<TaiexDto>(result.Meta, data);
            var response = new TaiexResponse<IPagingPayload<TaiexDto>>(payload)
            {
                Code = StatusCodes.Status200OK,
            };

            return response;
        });
    }
}