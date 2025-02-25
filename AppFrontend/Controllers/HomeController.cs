using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppFrontend.Models;
using AppFrontend.Services;

namespace AppFrontend.Controllers;

public class HomeController : Controller
{ 
   

    public HomeController()
    {
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
