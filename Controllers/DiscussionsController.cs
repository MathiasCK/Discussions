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
            TempData["ErrorMessage"] = "Discussions could not be fetched";
            return RedirectToAction("Index", "Home");
        }

        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
        }

        if (TempData.ContainsKey("SuccessMessage"))
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
        }

        return View(discussions);
    }

    public async Task<IActionResult> Details(string id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (TempData.ContainsKey("SuccessMessage"))
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
        }

        if(TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
        }

        if (discussion == null)
        {
            TempData["ErrorMessage"] = "Discussion could not be fetched";
            return RedirectToAction("Index", "Discussions");
        }

        return View(discussion);
    }

    public IActionResult Create()
    {
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Discussion discussion)
    {

        var sessionId = HttpContext.Session.GetString("UserId");
        var sessionEmail = HttpContext.Session.GetString("UserEmail");

        if (sessionId == null || sessionEmail == null)
        {
            var msg = "Could not create discussion: Session expired";
            _logger.LogError(msg);
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserEmail");

            TempData["ErrorMessage"] = msg;
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

        bool created = await _discussionsRepository.CreateDiscussion(newDiscussion, sessionEmail);

        if (!created)
        {
            TempData["ErrorMessage"] = "Discussion could not be created";
            return RedirectToAction("Create", "Discussions");
        }

        TempData["SuccessMessage"] = "Discussion created";
        return RedirectToAction("Details", "Discussions", new { id = newDiscussion.Id });
    }

    public async Task<IActionResult> Update(string id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (discussion == null)
        {
            TempData["ErrorMessage"] = "Discussion could not be fetched";
            return RedirectToAction("Index", "Discussions");
        }
        return View(discussion);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateConfirmed(Discussion discussion)
    {

        bool updated = await _discussionsRepository.Update(discussion);

        if (!updated)
        {
            TempData["ErrorMessage"] = "Discussion could not be updated";
            return RedirectToAction("Details", "Discussions", new { id = discussion.Id });
        }

        TempData["SuccessMessage"] = "Discussion updated";
        return RedirectToAction("Details", "Discussions", new { id = discussion.Id });
    }

    public async Task<IActionResult> Delete(string id)
    {

        var discussion = await _discussionsRepository.FetchDiscussion(id);

        if (discussion == null)
        {
            TempData["ErrorMessage"] = "Discussion could not be fetched";
            return RedirectToAction("Index", "Discussions");
        }
        return View(discussion);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        bool deleted = await _discussionsRepository.DeleteDiscussion(id);

        if (!deleted)
        {
            TempData["ErrorMessage"] = "Discussion could not be deleted";
            return RedirectToAction("Index", "Discussions");
        }

        TempData["SuccessMessage"] = "Discussion deleted";
        return RedirectToAction(nameof(Index));
    }

}

