namespace StockApi.Models
{
    public class AttachmentOptions
    {
        public string Path { get; set; } = string.Empty;
        public string MaxSize { get; set; } = string.Empty;
        public string MaxCount { get; set; } = string.Empty;
        public List<string> Extensions { get; set; } = new();
    }
}
