using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sample.AspNetMvc.Models;
using Sample.AspNetMvc.Models.Home;
using System.Diagnostics;

namespace Sample.AspNetMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
