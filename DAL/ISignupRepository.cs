using Discussions.Models;

namespace Discussions.DAL
{
	public interface ISignupRepository
	{
        void CreateSession(User user);
        Task<User?> CreateUser(User user);

    }
}

