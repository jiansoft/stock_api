using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions
{
    public class GeneralResponse<T> : IResponse
    {
        public int Code { get; set; }

        public Meta? Meta { get; set; }
        
        public Payload<T> Payload { get; set; }
        
        public string KeyWithPrefix()
        {
            return $"{nameof(GeneralResponse<T>)}";
        }
    }

    public struct Payload<T>(T data)
    {
        public T Data { get; set; } = data;
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
}
