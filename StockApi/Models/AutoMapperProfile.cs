using AutoMapper;
using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions.Stock.Details;

namespace StockApi.Models;

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
    }
}