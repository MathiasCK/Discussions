using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;

namespace Discussions.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public IActionResult Index()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            }

            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            var response = await _loginRepository.CheckUserCredentials(user);

            if (response != "OK" || !ModelState.IsValid)
            {
                ViewBag.ErrorMessage = response;
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

