using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StockApi.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnGet()
        {
        }
    }

}
