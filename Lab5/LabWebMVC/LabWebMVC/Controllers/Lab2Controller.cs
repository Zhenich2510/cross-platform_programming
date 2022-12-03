using System.Diagnostics;

using LabsClassLibrary;

using LabWebMVC.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabWebMVC.Controllers
{
    [Authorize] 
    public class Lab2Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Lab2Controller(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RunLab(string input)
        {
            if (!uint.TryParse(input, out uint N) || N <= 1 || N > 1000)
            {
                return Content("Число має бути між 1 та 1000");
            }

            var result = LabSequence.CountCombines(N);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}