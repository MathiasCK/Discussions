using Discussions.Models;

namespace Discussions.DAL
{
	public interface IDiscussionsRepository
	{
        Task<IEnumerable<Discussion>?> FetchDiscussions();
        Task<Discussion?> FetchDiscussion(int id);
        Task<bool> CreateDiscussion(Discussion discussion);
        Task<bool> Update(Discussion discussion);
        Task<bool> Delete(int id);
    }
}

