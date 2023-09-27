using Discussions.Models;

namespace Discussions.DAL
{
	public interface ISignupRepository
	{
        void SetSessionEmail(User user);
        Task<User?> CreateUser(User user);

    }
}

