using Discussions.Controllers;
using Discussions.Models;

namespace Discussions.DAL
{
	public class LoginRepository : ILoginRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public LoginRepository(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, DB db)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public void SetSessionEmail(User user)
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserEmail", user.Email);
                _logger.LogInformation("Sucsessfully set Session value UserEmail to: {userEmail}", user.Email);
            } catch (Exception e)
            {
                _logger.LogError("There was an error setting Session value UserEmail: {e}", e.Message);
            }
        }
 
	}
}

