using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Semana_7.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
