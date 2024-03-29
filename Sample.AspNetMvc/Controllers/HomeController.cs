using Microsoft.AspNetCore.Mvc;
using Sample.AspNetMvc.Models;
using System.Diagnostics;

namespace Sample.AspNetMvc.Controllers
{
    public class HomeController : Controller
    {
         public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
