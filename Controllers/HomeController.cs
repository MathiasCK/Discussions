using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        var user = new User();
        var userEmail = HttpContext.Session.GetString("UserEmail");

        user.Email = userEmail;

        return View(user);

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

