using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions;
public interface IPayload<T>
{
    public T Data { get; set; }
}

public interface IPagingPayload<T>
{
    /// <summary>
    /// 分頁相關資料
    /// </summary>
    public Meta Meta { get; set; } 
    
    public IEnumerable<T> Data { get; set; }
}

public struct GenerallyPagingPayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    public Meta Meta { get; set; } = meta;
    
    public IEnumerable<T> Data { get; set; } = data;
}

public struct Meta
{
    public long PageIndex { get; set; }
    public long PageSize { get; set; }
    public long PageCount { get; set; }
    public long RecordCount { get; set; }

    public Meta(long recordCount, long pageIndex, long pageSize)
    {
        RecordCount = recordCount;

        if (RecordCount <= 0) return;

        // 計算總頁數
        PageCount = (int)Math.Ceiling(recordCount / (decimal)pageSize);
        PageIndex = Math.Min(Math.Max(pageIndex, Constants.DefaultPageIndex), PageCount);
        PageSize = pageSize;
    }
}