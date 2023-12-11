namespace StockApi.Models.Exceptions;

public class MessageException(string message) : Exception(message);