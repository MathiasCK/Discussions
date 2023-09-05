using Microsoft.AspNetCore.Mvc;
using Discussions.Models;
using Discussions.DAL;

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

}

