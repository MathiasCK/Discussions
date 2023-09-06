using Discussions.Models;

namespace Discussions.DAL
{
	public interface ILoginRepository
	{
        void SetSessionEmail(User user);
        Task<User?> FetchOrCreateUser(string email);

    }
}

