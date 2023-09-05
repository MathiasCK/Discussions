using System.ComponentModel.DataAnnotations;

namespace Discussions.Models
{
	public class User
	{
        [Required]
        public string Email { get; set; } = string.Empty;
	}
}

