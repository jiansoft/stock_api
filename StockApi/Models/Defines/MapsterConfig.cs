using Mapster;
using Stock;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Twse;
using System.Reflection;

namespace StockApi.Models.Defines;

/// <summary>
/// 
/// </summary>
public static class MapsterConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_"></param>
    public static void RegisterMapsterConfiguration(this IServiceCollection _)
    {
        TypeAdapterConfig<IndexEntity, TaiexDto>
            .NewConfig()
            .Map(dest => dest.Date, src => DateOnly.FromDateTime(src.Date));

        TypeAdapterConfig<HolidaySchedule, HolidayScheduleDto>
            .NewConfig();

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}