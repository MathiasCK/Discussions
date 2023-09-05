using Discussions.Controllers;
using Discussions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Discussions.DAL
{
	public class LoginRepository : ILoginRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        private readonly DB _db;

        public LoginRepository(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, DB db)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _db = db;
        }

        public async void SetSessionEmail(User user)
        {
            try
            {
                var userExists = await FetchUser(user.Email);
                string sessionId = userExists?.Id ?? Guid.NewGuid().ToString();

                _httpContextAccessor.HttpContext?.Session.SetString("UserId", sessionId);
                _httpContextAccessor.HttpContext?.Session.SetString("UserEmail", user.Email);
                _logger.LogInformation("Sucsessfully set Session value 'UserEmail' to: {userEmail} and value 'UserId' to: {userId}", user.Email, sessionId);
            } catch (Exception e)
            {
                _logger.LogError("There was an error setting Session value UserEmail: {e}", e.Message);
            }
        }
        public async Task<User?> FetchUser(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email) ?? null;

        }
}
}

