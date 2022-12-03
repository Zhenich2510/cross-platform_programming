using System.Diagnostics;

using LabsClassLibrary;

using LabWebMVC.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabWebMVC.Controllers
{
    [Authorize]
    public class Lab1Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Lab1Controller(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RunLab(string input)
        {
            var result = Lab1Combination.CombineString(input);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}