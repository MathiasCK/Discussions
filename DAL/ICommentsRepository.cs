using System;
using Discussions.Models;

namespace Discussions.DAL
{
	public interface ICommentsRepository
	{
        Task<Discussion?> FetchDiscussion(string id);
        Task<bool> CreateComment(Comment comment);

    }
}

