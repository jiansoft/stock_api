namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個HTTP請求的基本介面，繼承自IHttpTransaction，用於實現各種HTTP請求。
/// </summary>
internal interface IRequest : IHttpTransaction;