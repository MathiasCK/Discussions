using System.ComponentModel.DataAnnotations;

namespace Discussions.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public int DiscussionId { get; set; }
        [StringLength(100)]
        public string Text { get; set; } = string.Empty;
        public User Author { get; set; }
		public DateTime Created { get; set; }

		public Comment(int id, int discussionId, string text, User author, DateTime created)
		{
			Id = id;
			DiscussionId = discussionId;
            Text = text;
			Author = author;
			Created = created;
		}
    }
}

