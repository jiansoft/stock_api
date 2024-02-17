using Mapster;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Models.Defines;

/// <summary>
/// 
/// </summary>
internal class MappingConfig : TypeAdapterConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static TypeAdapterConfig RegisterMappings()
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<IndexEntity, TaiexDto>()
            .Map(dest => dest.Date, src => DateOnly.FromDateTime(src.Date));
        
        return config;
    }
}