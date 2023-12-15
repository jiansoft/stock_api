namespace StockApi.Models.DataProviders;

public interface IDataResult<T>
{
    public IEnumerable<T> Entities { get; set; }
}