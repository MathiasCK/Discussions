
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using System.Runtime.Intrinsics.X86;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class DiscussionsController : Controller
{
    private Comment comment_11 = new Comment(11, 1, "This is comment 11", new User { Email = "john@doe.com" }, DateTime.Now);
    private Comment comment_12 = new Comment(12, 1, "This is comment 12", new User { Email = "mck@mail.no" }, DateTime.Now);

    private Comment comment_21 = new Comment(21, 2, "This is comment 21", new User { Email = "mck@mail.no" }, DateTime.Now);
    private Comment comment_22 = new Comment(22, 2, "This is comment 22", new User { Email = "john@doe.com" }, DateTime.Now);

    private Discussion discussion1 = new Discussion(1, "This is a header 1", "This is text 1", new User { Email = "mck@mail.no" }, DateTime.Now, DateTime.Now);
    private Discussion discussion2 = new Discussion(2, "This is a header 2", "This is text 2", new User { Email = "john@doe.com" }, DateTime.Now, DateTime.Now);

    private readonly List<Discussion> _discussions = new();

    public void init()
    {
        discussion1.Comments.Add(comment_11);
        discussion1.Comments.Add(comment_12);

        discussion2.Comments.Add(comment_21);
        discussion2.Comments.Add(comment_22);

        _discussions.Add(discussion1);
        _discussions.Add(discussion2);
    }

    public IActionResult Index()
    {
        init();
        return View(_discussions);
    }

    public IActionResult Details(int id)
    {
        init();

        var discussion = _discussions.Find(e => e.Id == id);

        if (discussion == null)
        {
            return BadRequest("Discussion not found");
        }
        return View(discussion);
    }

}

