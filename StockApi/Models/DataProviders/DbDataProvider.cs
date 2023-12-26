using jIAnSoft.Brook.Mapper;
using System.Data.Common;

namespace StockApi.Models.DataProviders;

public abstract class DbDataProvider : IDataProvider
{
    protected long GetOne(SqlMapper db, string table, string where, string join = "")
    {
        var sqlCount = $"SELECT COUNT(*) AS TotalCount FROM {table} {join} where {where}";
        var one = db.One<long>(sqlCount);

        return one;
    }

    protected long GetOne(SqlMapper db, string table, string join, string where, DbParameter[]? parameters)
    {

        var sqlCount = $"SELECT COUNT(*) AS TotalCount FROM {table} {join} {where}";
        var one = db.One<long>(sqlCount, parameters);

        return one;
    }
}