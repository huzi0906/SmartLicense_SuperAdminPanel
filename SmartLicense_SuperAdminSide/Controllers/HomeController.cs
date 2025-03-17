

namespace SmartLicense_SuperAdminSide.Controllers
{
    using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLicense_SuperAdminSide.Models;
using SmartLicense_SuperAdminSide.Services;
using System.Security.Claims;
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
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
            var admins = _adminService.GetAllAdmins();
            return View(admins);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "SuperAdmin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Dashboard");
            }
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.IsEnabled = true;
                Console.WriteLine($"Adding admin: {admin.FirstName} {admin.LastName}");
                _adminService.CreateAdmin(admin);
                return RedirectToAction("Dashboard");
            }
            Console.WriteLine("Model state invalid");
            return View(admin);
        }

        [HttpPost]
        public IActionResult EnableAdmin(string id)
        {
            _adminService.EnableAdmin(id);
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult DisableAdmin(string id)
        {
            _adminService.DisableAdmin(id);
            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}
