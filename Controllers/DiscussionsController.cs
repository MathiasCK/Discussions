using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.HttpResults;

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

    public async Task<IActionResult> Details(string id)
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
        Guid uuid = Guid.NewGuid();

        Discussion newDiscussion = new Discussion
        {
            Id = uuid.ToString(),
            Topic = discussion.Topic,
            Body = discussion.Body,
            Author = new User
            {
                Id = HttpContext.Session.GetString("UserId"),
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

    public async Task<IActionResult> Delete(string id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (discussion == null)
        {
            return BadRequest("Discussion not found");
        }
        return View(discussion);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        bool deleted = await _discussionsRepository.DeleteDiscussion(id);

        if (!deleted)
        {
            return BadRequest("Could not delete discussion");
        }

        return RedirectToAction(nameof(Index));
    }

}

