namespace StockApi.Models.Defines
{
    /// <summary>
    /// 
    /// </summary>
    public class AttachmentOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string MaxSize { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string MaxCount { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<string> Extensions { get; set; } = new();
    }
}
