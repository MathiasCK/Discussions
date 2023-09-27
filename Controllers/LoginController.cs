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
            var usr = await _loginRepository.CheckUserCredentials(user);

            if (usr == null)
            {
                ViewBag.ErrorMessage = "Invalid credentials";
                return View();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "There was an error logging in, please try again later";
                return View();
            }

            _loginRepository.CreateSession(usr);
            return RedirectToAction("Index", "Home");
        }
    }
}

