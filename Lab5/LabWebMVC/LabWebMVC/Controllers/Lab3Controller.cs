using System.Diagnostics;

using LabsClassLibrary;

using LabWebMVC.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabWebMVC.Controllers
{
    [Authorize] 
    public class Lab3Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Lab3Controller(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RunLab(string start1, string start2, string finish1, string finish2)
        {
            var result = LabPyatnashki.ComputeStepsCount(start1 + start2, finish1 + finish2);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}