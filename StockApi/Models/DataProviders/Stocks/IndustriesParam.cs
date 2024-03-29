﻿namespace StockApi.Models.DataProviders.Stocks;

internal struct IndustriesParam(IKey req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(IndustriesParam)}";
    }
}