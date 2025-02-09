using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartLicense_SuperAdminSide.Models;

namespace SmartLicense_SuperAdminSide.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult AddAdmin()
    {
        return View();
    }

    public IActionResult Logout()
    {
        // Assuming you are using cookie-based authentication
        // HttpContext.SignOutAsync(); // This method is used to sign out the user and invalidate the session cookie.

        return RedirectToAction("Login");
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Placeholder authentication logic
        if (username == "admin" && password == "admin123") // Example credentials
        {
            return RedirectToAction("Dashboard"); // Redirect to the dashboard after login
        }
        else
        {
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
