using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jjwebaspire.web.Pages
{
    public class WeatherForecastModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public WeatherForecastModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
