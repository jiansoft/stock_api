using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Stocks;
using StockApi.Models.HttpTransactions;
using StockApi.Models.HttpTransactions.Stock.Details;
using StockApi.Models.HttpTransactions.Stock.Dividend;
using StockApi.Models.HttpTransactions.Stock.Industry;

namespace StockApi.Models.Services;

public class StockService(StocksDataProvider sp, CacheDataProvider cp)
{
    /// <summary>
    /// Retrieves stock details response.
    /// </summary>
    /// <param name="request">The request object containing the necessary information to retrieve stock details.</param>
    /// <returns>A response object containing a list of stock information.</returns>
    public DetailsResponse<IEnumerable<DetailDto>> GetDetailsResponse(DetailsRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)), () =>
        {
            var param = new StocksParam(request);
            var result = sp.GetStocks(param);
            var data = result.Entities.Select(s => new DetailDto(s));

            return new DetailsResponse<IEnumerable<DetailDto>>
            {
                Code = StatusCodes.Status200OK,
                Meta = result.Meta,
                Payload = new Payload<IEnumerable<DetailDto>>(data)
            };
        });
    }

    /// <summary>
    /// 股票產業分類
    /// </summary>
    /// <param name="request">查詢參數</param>
    /// <returns>股票產業分類</returns>
    public IndustriesResponse<IEnumerable<IndustryDto>> GetIndustriesResponse(IndustriesRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(30)), () =>
        {
            var param = new IndustriesParam(request);
            var result = sp.GetIndustries(param);
            var data = result.Entities.Select(s => new IndustryDto(s));

            return new IndustriesResponse<IEnumerable<IndustryDto>>
            {
                Code = StatusCodes.Status200OK,
                Payload = new Payload<IEnumerable<IndustryDto>>(data)
            };
        });
    }
    
    /// <summary>
    /// 股票歷年發放股利
    /// </summary>
    /// <param name="request">查詢參數</param>
    /// <returns>股票歷年發放股利</returns>
    public DividendResponse<IEnumerable<DividendDto>> GetDividendResponse(DividendRequest request)
    {
        return cp.GetOrSet(request.KeyWithPrefix(), CacheDataProvider.NewOption(TimeSpan.FromDays(1)), () =>
        {
            var param = new DividendParam(request);
            var result = sp.GetDividend(param);
            var data = result.Entities.Select(s => new DividendDto(s));

            return new DividendResponse<IEnumerable<DividendDto>>
            {
                Code = StatusCodes.Status200OK,
                Payload = new Payload<IEnumerable<DividendDto>>(data)
            };
        });
    }
}