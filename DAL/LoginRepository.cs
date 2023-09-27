using Discussions.Controllers;
using Discussions.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;

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

        public async Task<String?> CheckUserCredentials(User user)
        {
            try
            {
                var usr = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (usr == null)
                {
                    return "No user registered with email " + user.Email;
                }

                var valid = BCrypt.Net.BCrypt.Verify(user.Password, usr.Password);

                return valid ? "OK" : "Password is incorrect";
            }
            catch (Exception e)
            {
                _logger.LogError("[LoginRepository]: There was an fetching user with email '{email}' : {e}", user.Email, e.Message);
                throw new Exception("[LoginRepository]: There was an fetching creating user  with email: " + user.Email + " : " + e.Message);
            }
        }

        public async void SetSessionEmail(User user)
        {
            try
            {
                _httpContextAccessor.HttpContext?.Session.SetString("UserId", user.Id);
                _httpContextAccessor.HttpContext?.Session.SetString("UserEmail", user.Email);
                _logger.LogInformation("[LoginRepository]: Sucsessfully set Session value 'UserEmail' to: {userEmail} and value 'UserId' to: {userId}", user.Email, user.Id);
            } catch (Exception e)
            {
                _logger.LogError("[LoginRepository]: There was an error setting Session value UserEmail: {e}", e.Message);
            }
        }
    }
}

