using Discussions.Models;

namespace Discussions.DAL
{
	public interface ISignupRepository
	{
        void CreateSession(User user);
        Task<String> CreateUser(User user);

    }
}

