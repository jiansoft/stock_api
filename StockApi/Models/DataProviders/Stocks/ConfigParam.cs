namespace StockApi.Models.DataProviders.Config
{
    public struct ConfigParam(string key) : IKey
    {
        public string Key { get; set; } = key;
    
        public string KeyWithPrefix()
        {
            return $"{nameof(ConfigParam)}";
        }
    }
}