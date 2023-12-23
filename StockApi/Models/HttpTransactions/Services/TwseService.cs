using StockApi.Models.DataProviders;
using StockApi.Models.DataProviders.Index;
using StockApi.Models.HttpTransactions.Twse;

namespace StockApi.Models.HttpTransactions.Services;

public class TwseService(StocksDataProvider sp, CacheDataProvider cp)
{
    public IResponse<IPagingPayload<TaiexDto>> GetTaiexResponse(TaiexRequest req)
    {
        return cp.GetOrSet(req.KeyWithPrefix(), CacheDataProvider.NewOption(Utils.GetNextTimeDiff(15)), () =>
        {
            var param = new IndexParam(req);
            var result = sp.GetIndex(param);
            var data = result.Entities.Select(s => new TaiexDto(s));
            var payload = new TaiexPayload<TaiexDto>(result.Meta, data);
            var response = new TaiexResponse<IPagingPayload<TaiexDto>>(payload)
            {
                Code = StatusCodes.Status200OK,
            };

            return response;
        });
    }
}