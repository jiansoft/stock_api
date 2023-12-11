using jIAnSoft.Brook.Mapper;

namespace StockApi.Models.DataProviders;

public abstract class DbDataProvider : IDataProvider
{
    protected long GetOne(SqlMapper db, string table, string where)
    {
        var sqlCount = $"SELECT COUNT(*) AS TotalCount FROM {table} where {where}";
        var one = db.One<long>(sqlCount);
        
        return one;
    }

    /*
    protected long GetOne(string table, string where)
    {
        using var db = Brook.Load("stock");
        return GetOne(db, table, where);
    }*/
}