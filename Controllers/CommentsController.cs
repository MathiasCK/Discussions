using Discussions.DAL;
using Discussions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discussions.Controllers
{
    [ServiceFilter(typeof(UserSessionFiler))]
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsController(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<IActionResult> Create(string discussionId)
        {
            var discussion = await _commentsRepository.FetchDiscussion(discussionId);

            if (discussion == null)
            {
                TempData["ErrorMessage"] = "Discussion could not be fetched";
                return RedirectToAction("Details", "Discussions", new { id = discussionId });
            }
            return View(discussion);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(string comment, string discussionId)
        {

            var newComment = new Comment();

            newComment.Id = Guid.NewGuid().ToString();
            newComment.DiscussionId = discussionId;
            newComment.Text = comment;
            newComment.Created = DateTime.Now;

            bool created = await _commentsRepository.CreateComment(newComment);

            if (!created)
            {
                TempData["ErrorMessage"] = "Comment could not be created";
                return RedirectToAction("Details", "Discussions", new { id = discussionId });
            }

            TempData["SuccessMessage"] = "Comment created";
            return RedirectToAction("Details", "Discussions", new { id = discussionId });
        }

        public async Task<IActionResult> Delete(string commentId)
        {
            var comment = await _commentsRepository.FetchComment(commentId);

            if (comment == null)
            {
                TempData["ErrorMessage"] = "Comment could not be fetched";
                return RedirectToAction("Index", "Discussions");
            }
            return View(comment);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(string id, string discussionId)
        {
            bool deleted = await _commentsRepository.DeleteComment(id);

            if (!deleted)
            {
                TempData["ErrorMessage"] = "Could not delete comment";
                return RedirectToAction("Details", "Discussions", new { id = discussionId });
            }

            TempData["SuccessMessage"] = "Comment deleted";
            return RedirectToAction("Details", "Discussions", new { id = discussionId });
        }

    }
}

