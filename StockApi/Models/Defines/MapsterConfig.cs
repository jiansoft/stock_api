using Mapster;
using Stock;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Models.Defines;

internal abstract class MapsterConfig : TypeAdapterConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static TypeAdapterConfig RegisterMappings()
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<IndexEntity, TaiexDto>().Map(dest => dest.Date, src => DateOnly.FromDateTime(src.Date));
        config.NewConfig<HolidaySchedule, HolidayScheduleDto>();
        config.NewConfig<StockEntity, DetailDto>();
        // CreateMap<StockEntity, DetailDto>();
        return config;
    }
}