namespace StockApi.Models.HttpTransactions.Stock.Details
{
    public class DetailsResponse : GeneralResponse<IEnumerable<DetailDto>>
    {
        public DetailsResponse(Meta meta, IEnumerable<DetailDto> data)
        {
            Meta = meta;
            Payload = new Payload<IEnumerable<DetailDto>>(data);
        }
    }
}
