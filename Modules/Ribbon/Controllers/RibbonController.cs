using Microsoft.AspNetCore.Mvc;
using Ribbon.Models;
using System.Diagnostics;

namespace Ribbon.Controllers
{
    public class RibbonController : Controller
    {
        private readonly ILogger<RibbonController> _logger;

        public RibbonController(ILogger<RibbonController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}