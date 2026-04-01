using Arebis.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sample.AspNetMvc.Models;
using Sample.AspNetMvc.Models.Home;
using System.Diagnostics;

namespace Sample.AspNetMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(IndexModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> Translations([FromServices] ITranslationService translator)
        {
            var model = new TranslationsModel();
            model.Translations.Add(new string?[] { "<h2><strong>Topic 1: Managen van shows in de toekomst</strong></h2>", null });
            model.Translations.Add(new string?[] { "<h2><strong>Topic 2: Registratie periode</strong></h2>", null });
            model.Translations.Add(new string?[] { "<h2><strong>Topic 3: Je tevredenheid over de dag van de show</strong></h2>", null });
            model.Translations.Add(new string?[] { "<h2><strong>Topic 4: Frustraties</strong></h2>", null });
            model.Translations.Add(new string?[] { "Wat was deze frustratie?", null });

            var result = (await translator.TranslateAsync("nl", "fr", "text/html", model.Translations.Select(t => t[0] ?? "").ToArray())).ToList();

            for(int i=0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
                model.Translations[i][1] = result[i];
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
