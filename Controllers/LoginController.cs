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
        public IActionResult Index(User user)
        {

            if (ModelState.IsValid)
            {
                _loginRepository.SetSessionEmail(user);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}

