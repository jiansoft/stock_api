using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Config;

public class ConfigResult(ConfigEntity entity)
{
    public ConfigEntity Entity { get; set; } = entity;
}