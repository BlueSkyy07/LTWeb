using Microsoft.AspNetCore.Mvc;

namespace SV20T1020095.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
