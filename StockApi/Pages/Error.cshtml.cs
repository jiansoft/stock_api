using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace StockApi.Pages
{
    /// <summary>
    /// 
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
