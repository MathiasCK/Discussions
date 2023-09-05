using Discussions.Controllers;
using Discussions.Models;

namespace Discussions.DAL
{
	public class HomeRepository : IHomeRepository
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeRepository(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public bool ValidateUserSession(User user)
		{
            try
            {
                var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");

                if (string.IsNullOrEmpty(userEmail))
                {
                    throw new Exception("UserEmail is null or empty");
                }

                user.Email = userEmail;
                return true;

            } catch (Exception e)
            {
                _logger.LogError("[HomeRepository]: There was an error getting value UserEmail from session storage: {e}", e.Message);
                return false;
            }
        }
	}
}

