using System.ComponentModel.DataAnnotations;

namespace Discussions.Models
{
	public class Discussion
	{
		
		public int Id { get; set; }
		[StringLength(50)]
        public string Header { get; set; } = string.Empty;
        [StringLength(100)]
        public string Body { get; set; } = string.Empty;
        public User Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

		public Discussion(int id, string header, string body, User author, DateTime created, DateTime updated)
		{
			Id = id;
			Header = header;
			Body = body;
			Author = author;
			Created = created;
			Updated = updated;
		}
	}
}

