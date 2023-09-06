using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class DiscussionsController : Controller
{
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly ILogger<DiscussionsRepository> _logger;

    public DiscussionsController(IDiscussionsRepository discussionsRepository, ILogger<DiscussionsRepository> logger)
    {
        _discussionsRepository = discussionsRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var discussions = await _discussionsRepository.FetchDiscussions();

        if (discussions == null)
        {
            return BadRequest("Could not fetch discussions");
        }

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

        var sessionId = HttpContext.Session.GetString("UserId");
        var sessionEmail = HttpContext.Session.GetString("UserEmail");

        if (sessionId == null || sessionEmail == null)
        {
            _logger.LogError("Could not create discussion: Session expired");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.GetString("UserEmail");
            return RedirectToAction("Index", "Login");
        }

        Discussion newDiscussion = new()
        {
            Id = Guid.NewGuid().ToString(),
            Topic = discussion.Topic,
            Body = discussion.Body,
            Created = DateTime.Now,
            Updated = DateTime.Now,
        };

        bool created = await _discussionsRepository.CreateDiscussion(newDiscussion, sessionId);

        if (!created)
        {
            return BadRequest("Could not create discussion");
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(string id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (discussion == null)
        {
            return BadRequest("Discussion not found");
        }
        return View(discussion);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateConfirmed(Discussion discussion)
    {

        bool deleted = await _discussionsRepository.Update(discussion);

        if (!deleted)
        {
            return BadRequest("Could not delete discussion");
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

