using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

using LabWebMVC.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> PrivacyAsync()
        {
            var httpClient = new HttpClient();
            var name = User.FindFirst("name")!.Value;
            var dic = await httpClient.GetFromJsonAsync<Dictionary<string, string>>("http://192.168.88.212:5001/Account/GetUserInfo?user=" + name);


            return View("Privacy", dic);
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}