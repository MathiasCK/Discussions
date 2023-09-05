using Discussions.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussions.DAL
{
	public class CommentsRepository : ICommentsRepository
	{
        private readonly DB _db;
        private readonly ILogger<DiscussionsRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsRepository(IHttpContextAccessor httpContextAccessor, DB db, ILogger<DiscussionsRepository> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _logger = logger;
        }

        public async Task<bool> CreateComment(Comment comment)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

                var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (existingUser == null)
                {
                    throw new Exception("[CommentsRepository]: Failed to set comment Author with id: " + userId);
                }

                comment.Author = existingUser;

                _db.Comments.Add(comment);
                await _db.SaveChangesAsync();
                _logger.LogInformation("[CommentsRepository]: Successfully created comment: '{comment}'", comment);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[DiscussionsRepository]: Could not create comment {comment} - {e}", comment, e.Message);
                return false;
            }
        }

        public async Task<Discussion?> FetchDiscussion(string id)
        {
            try
            {
                return await _db.Discussions.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError("[DiscussionsRepository]: Could not fetch discusstion with id: '{id}' - {e}", id, e.Message);
                return null;
            }
        }
    }
}

