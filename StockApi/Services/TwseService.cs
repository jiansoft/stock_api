using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StockApi.Models;
using StockApi.Models.DataProviders;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Services;

/// <summary>
/// 提供台灣證券交易所相關服務的類別，負責從數據來源獲取和處理台灣加權股價指數（TAIEX）等相關數據。
/// </summary>
/// <param name="cdp">緩存數據提供者，用於緩存台灣證券交易所的數據，提高數據訪問效率。</param>
/// <param name="mapper">物件對應設定，用於在不同的數據模型之間進行轉換。</param>
public class TwseService(CacheDataProvider cdp,  IMapper mapper)
{
    /// <summary>
    /// 獲取台灣加權股價指數的回應。此方法從數據源中獲取台灣加權股價指數的數據，並透過緩存提高數據訪問效率。
    /// </summary>
    /// <param name="req">包含查詢條件的台灣加權股價指數請求對象。</param>
    /// <param name="sc">股票數據的上下文，用於訪問數據庫。</param>
    /// <returns>包含台灣加權股價指數數據的回應對象。</returns>
    internal async Task<IHttpTransaction> GetTaiexAsync(TaiexRequest req, StockContext sc)
    {
        return await cdp.GetOrSetAsync(
            req.KeyWithPrefix(),
            CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)),
            async () =>
            {
                var totalRecords = await sc.Indexes.LongCountAsync(w => w.Category == req.Category);
                var meta = new Meta(totalRecords, req.RequestedPage, req.RecordsPerPage);
                var result = await sc.Indexes
                    .Where(w => w.Category == req.Category)
                    .OrderByDescending(ob => ob.Date)
                    .Skip(meta.Offset)
                    .Take(meta.RecordsPerPage)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
                var data = mapper.Map<IEnumerable<IndexEntity>,IEnumerable<TaiexDto>>(result);
                var payload = new PagingPayload<TaiexDto>(meta, data);
                var response = new TaiexResponse<IPagingPayload<TaiexDto>>(StatusCodes.Status200OK, payload);

                return response;
            });
    }
}