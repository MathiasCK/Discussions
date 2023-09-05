using Discussions.Models;

namespace Discussions.DAL
{
	public interface IHomeRepository
	{
        public bool ValidateUserSession(User user);

    }
}

