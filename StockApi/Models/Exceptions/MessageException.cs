namespace StockApi.Models.Exceptions;

/// <summary>
/// 表示一個帶有訊息的異常。
/// </summary>
/// <param name="message"></param>
public class MessageException(string message) : Exception(message);