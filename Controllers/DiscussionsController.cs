using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class DiscussionsController : Controller
{
    private readonly IDiscussionsRepository _discussionsRepository;

    public DiscussionsController(IDiscussionsRepository discussionsRepository)
    {
        _discussionsRepository = discussionsRepository;
    }

    public async Task<IActionResult> Index()
    {
        var discussions = await _discussionsRepository.FetchDiscussions();
        return View(discussions);
    }

    public async Task<IActionResult> Details(int id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (discussion == null)
        {
            return BadRequest("Discussion not found");
        }
        return View(discussion);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Discussion discussion)
    {

        Discussion newDiscussion = new Discussion
        {
            Id = 2222222,
            Header = discussion.Header,
            Body = discussion.Body,
            Author = new User
            {
                Id = 2222222,
                Email = HttpContext.Session.GetString("UserEmail"),
            },
            Created = DateTime.Now,
            Updated = DateTime.Now,
    
        };

        bool created = await _discussionsRepository.CreateDiscussion(newDiscussion);

        if (!created)
        {
            return BadRequest("Could not create discussion");
        }

        return RedirectToAction(nameof(Index));
    }

}

