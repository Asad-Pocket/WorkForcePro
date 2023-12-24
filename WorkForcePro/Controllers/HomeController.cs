using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkForcePro.Models;

namespace WorkForcePro.Controllers
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
        [HttpPost]
        public IActionResult StoreSelectedCompany(string companyId, string companyName)
        {
            // Store the selected company information in cookies
            Response.Cookies.Append("SelectedCompanyId", companyId);
            Response.Cookies.Append("SelectedCompanyName", companyName);

            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the selected company details from the cookie
            Response.Cookies.Delete("SelectedCompanyId");
            Response.Cookies.Delete("SelectedCompanyName");
            return Json(new { success = true });
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
