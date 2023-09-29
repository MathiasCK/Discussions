using Microsoft.AspNetCore.Mvc;

namespace Discussions.Controllers
{
    [ServiceFilter(typeof(UserSessionFiler))]
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirm()
        {

            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserEmail");

            TempData["SuccessMessage"] = "Logout successful";
            return RedirectToAction("Index", "Login");
        }
    }
}

