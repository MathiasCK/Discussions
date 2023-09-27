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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            var validCredentials = await _loginRepository.CheckUserCredentials(user);

            if (ModelState.IsValid && validCredentials == "OK")
            {
                _loginRepository.SetSessionEmail(user);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = validCredentials;
            return View(user);
        }
    }
}

