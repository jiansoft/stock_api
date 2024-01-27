using jIAnSoft.Brook.Mapper;
using System.Data.Common;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 
/// </summary>
public abstract class DbDataProvider : IDataProvider
{
    /// <summary>
    /// 從數據庫中獲取滿足指定條件的記錄總數。
    /// </summary>
    /// <param name="db">SqlMapper 實例。</param>
    /// <param name="table">表名稱。</param>
    /// <param name="where">條件語句。</param>
    /// <param name="join">連接語句（可選）。</param>
    /// <returns>記錄總數。</returns>
    protected long GetOne(SqlMapper db, string table, string where, string join = "")
    {
        var sqlCount = $"SELECT COUNT(*) AS TotalCount FROM {table} {join} where {where}";
        var one = db.One<long>(sqlCount);

        return one;
    }

    /// <summary>
    /// 從數據庫中獲取滿足指定條件的記錄總數。
    /// </summary>
    /// <param name="db">SqlMapper 實例。</param>
    /// <param name="table">表名稱。</param>
    /// <param name="join">連接語句。</param>
    /// <param name="where">條件語句。</param>
    /// <param name="parameters">參數（可選）。</param>
    /// <returns>記錄總數。</returns>
    protected long GetOne(SqlMapper db, string table, string join, string where, DbParameter[]? parameters)
    {
        var sqlCount = $"SELECT COUNT(*) AS TotalCount FROM {table} {join} {where}";
        var one = db.One<long>(sqlCount, parameters);

        return one;
    }
}