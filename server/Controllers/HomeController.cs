using System;
using Microsoft.AspNetCore.Mvc;

namespace EDziennik.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
