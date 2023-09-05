
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.PortableExecutable;

namespace Discussions.Controllers;

public class DiscussionsController : Controller
{

    public IActionResult Index()
    {
        var discusions = new List<Discussion>();

        var user1 = new User
        {
            Email = "mck@mail.no"
        };

        var user2 = new User
        {
            Email = "john@doe.com"
        };

        var discussion1 = new Discussion(1, "This is a header 1", "This is text 1", user1, DateTime.Now, DateTime.Now);

        discusions.Add(discussion1);

        var discussion2 = new Discussion(2, "This is a header 2", "This is text 2", user2, DateTime.Now, DateTime.Now);

        discusions.Add(discussion2);

        return View(discusions);
    }

}

