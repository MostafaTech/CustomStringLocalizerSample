using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomStringLocalizerSample.Web.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace CustomStringLocalizerSample.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;
        public HomeController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            var title = _localizer.GetString("contact");
            ViewData["Message"] = title;

            var vm = new ContactViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Contact([FromForm] ContactViewModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                // do something
                message = $"Hey, {model.Name}. we got your message.";
            }
            else
            {
                message = $"form has {ModelState.ErrorCount} errors: {Environment.NewLine}";
                foreach (var e in ModelState)
                    message += $"- {e.Key} = {e.Value} {Environment.NewLine}";
            }
            return View(model);
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

        public IActionResult SetCulture(string culture)
        {
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            Response.Cookies.Append(".AspNetCore.Culture", cookieValue);
            return Redirect("/");
        }
    }
}
