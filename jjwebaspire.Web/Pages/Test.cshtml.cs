using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace jjwebaspire.web.Pages
{
    public class TestModel : PageModel
    {
        private readonly IHttpClientFactory _cl;
        private readonly ILogger<TestModel> _logger;

        public TestModel(IHttpClientFactory clientFactory, ILogger<TestModel> logger)
        {
            _cl = clientFactory;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            ViewData["Message"] = "Test API page.";

            var host = Dns.GetHostName();
            ViewData["Host"] = host;

            try
            {
                // call service
                var client = _cl.CreateClient("jjapi");
                string result = await client.GetStringAsync("/api/values");
                ViewData["ServiceUrl"] = client.BaseAddress.ToString();
                ViewData["ApiResult"] = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling API"); // Log error using logger
                ViewData["ApiResult"] = "Error calling: " + ex.Message;
            }

            string hcontact = "";
            foreach (var h in Request.Headers)
            {
                hcontact += h.Key + "=" + h.Value + " | ";
            }
            ViewData["Headers"] = hcontact;
        }
    }
}
