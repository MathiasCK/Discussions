using Discussions.Models;

namespace Discussions.DAL
{
	public interface ILoginRepository
	{
        Task<User?> CheckUserCredentials(User user);
        void CreateSession(User user);

    }
}

