﻿using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

internal  struct IndexParam(AbstractPagingRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public uint PageIndex { get; set; } = req.RequestedPage;
    public uint PageSize { get; set; } = req.RecordsPerPage;
    public string Category { get; set; } = "TAIEX";

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndexParam)}:{PageIndex}-{PageSize}";
    }
}