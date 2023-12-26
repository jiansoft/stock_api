using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

public class ConfigResult(ConfigEntity entity)
{
    public ConfigEntity Entity { get; set; } = entity;
}