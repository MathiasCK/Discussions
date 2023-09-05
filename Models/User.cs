using System.ComponentModel.DataAnnotations;

namespace Discussions.Models
{
	public class User
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
	}
}

