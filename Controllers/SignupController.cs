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

            var usr = await _signupRepository.CreateUser(user);

            if (usr == null)
            {
                ViewBag.ErrorMessage = "User with email " + user.Email + " already exists";
                return View();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "There was an error creating user, please try again later";
                return View();
            }

            _signupRepository.CreateSession(usr);

            return RedirectToAction("Index", "Home");


        }
    }
}

