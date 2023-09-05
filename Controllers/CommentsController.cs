using Discussions.DAL;
using Discussions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discussions.Controllers
{
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
                return BadRequest("Could not find discussion with id: " + discussionId);
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
                return BadRequest("Could not create comment");
            }


            return RedirectToAction("Details", "Discussions", new { id = discussionId });
        }

        public async Task<IActionResult> Delete(string commentId)
        {
            var comment = await _commentsRepository.FetchComment(commentId);

            if (comment == null)
            {
                return BadRequest("Could not find comment with id: " + commentId);
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
                return BadRequest("Could not delete discussion");
            }

            return RedirectToAction("Details", "Discussions", new { id = discussionId });
        }

    }
}

