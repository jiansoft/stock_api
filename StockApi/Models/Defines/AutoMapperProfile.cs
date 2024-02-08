using AutoMapper;
using Stock;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;
using StockApi.Models.HttpTransactions.Stock.HolidaySchedule;
using StockApi.Models.HttpTransactions.Stock.Industry;
using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;
using StockApi.Models.HttpTransactions.Stock.Revenue;

namespace StockApi.Models.Defines;

/// <summary>
/// 用於組態 AutoMapper 對應組態的類別。
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// 初始化 AutoMapper 對應組態。
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<StockEntity, DetailDto>();
        CreateMap<RevenueEntity, RevenueDto>();
        CreateMap<IndustryEntity, IndustryDto>();
        CreateMap<DailyQuoteEntity, HistoricalDailyQuoteDto>()
            .ForMember(
                dest => dest.MaximumPriceInYearDateOn,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.MaximumPriceInYearDateOn)))
            .ForMember(
                dest => dest.MinimumPriceInYearDateOn,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.MinimumPriceInYearDateOn)));
        CreateMap<DailyQuoteEntity, LastDailyQuoteDto>()
            .ForMember(
                dest => dest.MaximumPriceInYearDateOn,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.MaximumPriceInYearDateOn)))
            .ForMember(
                dest => dest.MinimumPriceInYearDateOn,
                opt => opt.MapFrom(src => DateOnly.FromDateTime(src.MinimumPriceInYearDateOn)));

        CreateMap<HolidaySchedule, HolidayScheduleDto>();
    }
    
}