using Discussions.Models;

namespace Discussions.DAL
{
	public interface IDiscussionsRepository
	{
        Task<IEnumerable<Discussion>?> FetchDiscussions();
        Task<Discussion?> FetchDiscussion(int id);
    }
}

