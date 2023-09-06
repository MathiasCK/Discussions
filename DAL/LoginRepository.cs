using Discussions.Controllers;
using Discussions.Models;
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
                var userExists = await FetchOrCreateUser(user.Email) ?? throw new Exception("500 - Database error");

                _httpContextAccessor.HttpContext?.Session.SetString("UserId", userExists.Id);
                _httpContextAccessor.HttpContext?.Session.SetString("UserEmail", userExists.Email);
                _logger.LogInformation("[LoginRepository]: Sucsessfully set Session value 'UserEmail' to: {userEmail} and value 'UserId' to: {userId}", user.Email, userExists.Id);
            } catch (Exception e)
            {
                _logger.LogError("[LoginRepository]: There was an error setting Session value UserEmail: {e}", e.Message);
            }
        }
        public async Task<User?> FetchOrCreateUser(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                try
                {
                    var newUser = new User
                    {
                        Email = email,
                        Id = Guid.NewGuid().ToString(),
                    };

                    _db.Users.Add(newUser);
                    await _db.SaveChangesAsync();

                    _logger.LogInformation("[LoginRepository]: Sucsessfully created user with email: '{email}' and id: '{id}'", newUser.Email, newUser.Id);
                    return newUser;
                } catch (Exception e)
                {
                    _logger.LogInformation("[LoginRepository]: There was an error creating user  with email: '{email}' - {e}", email, e.Message);
                    throw new Exception("[LoginRepository]: There was an error creating user  with email: " + email + " - " + e.Message);
                }
            }

            return user;
        }
}
}

