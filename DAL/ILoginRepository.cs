using Discussions.Models;

namespace Discussions.DAL
{
	public interface ILoginRepository
	{
        Task<String?> CheckUserCredentials(User user);
        void SetSessionEmail(User user);

    }
}

