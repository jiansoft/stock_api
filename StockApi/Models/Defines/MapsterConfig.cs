using Mapster;
using Stock;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Models.Defines;

internal abstract class MapsterConfig : TypeAdapterConfig
{
    // 靜態欄位來儲存組態實例
    private static readonly TypeAdapterConfig Config;

    // 使用靜態建構函式進行初始化
    static MapsterConfig()
    {
        Config = new TypeAdapterConfig();
        
        // 註冊對應組態
        Config.NewConfig<IndexEntity, TaiexDto>().Map(dest => dest.Date, src => DateOnly.FromDateTime(src.Date));

        Config.NewConfig<HolidaySchedule, HolidayScheduleDto>();
        Config.NewConfig<StockEntity, DetailDto>();
        Config.NewConfig<StockIndustryEntity, IndustryDto>();

        Config.NewConfig<DividendEntity, DividendDto>()
            .Map(dest => dest.CashDividendYield, src => Math.Round(src.CashDividendYield, 2));
    }
    
    // 提供一個公共的靜態方法來獲取組態實例
    public static TypeAdapterConfig GetConfig()
    {
        return Config;
    }
}