using System;
using Discussions.Models;

namespace Discussions.DAL
{
	public interface ILoginRepository
	{
        public void SetSessionEmail(User user);

    }
}

