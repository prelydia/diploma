using diploma.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace diploma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult Index(int id)
        {
            //Console.WriteLine(id);

            // Текущий пользователь
            var t = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var tt = db.Users.Where(s => s.Login.Equals(t)).FirstOrDefault();
            ViewBag.Name = tt.Surname + ' ' + tt.Name;

            ViewBag.Methods = db.Methods.ToList();

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

        public IActionResult Info()
        {
            // Текущий пользователь
            var t = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var tt = db.Users.Where(s => s.Login.Equals(t)).FirstOrDefault();
            ViewBag.Name = tt.Surname + ' ' + tt.Name;

            return View();
        }

        public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }

            //return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
            return CultureInfo.CurrentCulture.Name;
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult TestButton()
        {
            var T = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return Content(T);
        }

    }
}
