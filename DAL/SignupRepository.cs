using Discussions.Controllers;
using Discussions.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussions.DAL
{
  public class SignupRepository : ISignupRepository
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SignupController> _logger;
    private readonly DB _db;

    public SignupRepository(IHttpContextAccessor httpContextAccessor, ILogger<SignupController> logger, DB db)
    {
      _httpContextAccessor = httpContextAccessor;
      _logger = logger;
      _db = db;
    }

    public async Task<User?> CreateUser(User user)
    {
      try
      {
        var userExists = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (userExists != null)
        {
          return null;
        }

        user.Id = Guid.NewGuid().ToString();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        _logger.LogInformation("[SignupRepository]: Successfully created user: '{email}'", user.Email);

        return user;
      } catch (Exception e)
      {
        _logger.LogError("[SignupRepository]: There was an error creating user with email '{email}' : {e}", user.Email, e.Message);
        throw new Exception("[SignupRepository]: There was an creating user  with email: " + user.Email + " : " + e.Message);
      }
    }

    public void SetSessionEmail(User user)
    {
      try
      {
        _httpContextAccessor.HttpContext?.Session.SetString("UserId", user.Id);
        _httpContextAccessor.HttpContext?.Session.SetString("UserEmail", user.Email);
        _logger.LogInformation("[SignupRepository]: Sucsessfully set Session value 'UserEmail' to: {userEmail} and value 'UserId' to: {userId}", user.Email, user.Id);
      }
      catch (Exception e)
      {
        _logger.LogError("[SignupRepository]: There was an error setting Session value UserEmail: {e}", e.Message);
      }
    }
  }
}