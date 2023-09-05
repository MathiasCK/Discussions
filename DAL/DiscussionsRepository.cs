using Discussions.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussions.DAL
{
    public class DiscussionsRepository : IDiscussionsRepository
    {
        private readonly DB _db;
        private readonly ILogger<DiscussionsRepository> _logger;

        public DiscussionsRepository(DB db, ILogger<DiscussionsRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Discussion>?> FetchDiscussions()
        {
            try
            {
                return await _db.Discussions.ToListAsync();
            } catch (Exception e)
            {
                _logger.LogError("[DiscussionsRepository]: Failed to fetch all discussions: {e}", e.Message); ;
                return null;
            }
        }

        public async Task<Discussion?> FetchDiscussion(int id)
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

