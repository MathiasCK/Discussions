
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using System.Runtime.Intrinsics.X86;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class DiscussionsController : Controller
{

    private readonly List<Discussion> _discussions = new List<Discussion>
    {
        new Discussion(1, "This is a header 1", "This is text 1", new User { Email = "mck@mail.no" }, DateTime.Now, DateTime.Now),
        new Discussion(2, "This is a header 2", "This is text 2", new User { Email = "john@doe.com" }, DateTime.Now, DateTime.Now)
    };
    

    public IActionResult Index()
    {

        return View(_discussions);
    }

    public IActionResult Details(int id)
    {
        var discussion = _discussions.Find(e => e.Id == id);

        if (discussion == null)
        {
            return BadRequest("Discussion not found");
        }
        return View(discussion);
    }

}

