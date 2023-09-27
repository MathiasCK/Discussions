using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;

namespace Discussions.Controllers
{
    public class SignupController : Controller
    {

        private readonly ISignupRepository _signupRepository;

        public SignupController(ISignupRepository signupRepository)
        {
            _signupRepository = signupRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user, string confirmPassword)
        {
            if (user.Password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match!";
                return View();
            }

            var response = await _signupRepository.CreateUser(user);

            if (response != "OK" || !ModelState.IsValid)
            {
                ViewBag.ErrorMessage = response;
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

