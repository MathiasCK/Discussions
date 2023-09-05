using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;

namespace Discussions.Controllers;

public class HomeController : Controller
{

    private readonly IHomeRepository _homeRepository;

    public HomeController(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }

    public IActionResult Index()
    {
        var user = new User();

        var validUser = _homeRepository.ValidateUserSession(user);

        if (validUser)
        {
            return View(user);
        }

        return RedirectToAction("Index", "Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

