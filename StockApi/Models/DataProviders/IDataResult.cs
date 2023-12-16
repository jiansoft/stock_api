namespace StockApi.Models.DataProviders;

public interface IDataResult<T>
{
    public T Result { get; set; }
}