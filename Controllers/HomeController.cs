using System;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        // Diğer action metodlarını buraya ekleyebilirsin
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
        // Ve diğer action metodlarını buraya ekleyebilirsin
        
    }
}