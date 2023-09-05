using Discussions.Models;

namespace Discussions.DAL
{
	public class DbInit
	{

        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            DB context = serviceScope.ServiceProvider.GetRequiredService<DB>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Guid uuidUser1 = Guid.NewGuid();
            Guid uuidUser2 = Guid.NewGuid();

            Guid uuidDiscussion1 = Guid.NewGuid();
            Guid uuidDiscussion2 = Guid.NewGuid();

            Guid uuidComment1 = Guid.NewGuid();
            Guid uuidComment2 = Guid.NewGuid();
            Guid uuidComment3 = Guid.NewGuid();
            Guid uuidComment4 = Guid.NewGuid();

            User user1 = new User
            {
                Id = uuidUser1.ToString(),
                Email = "mck@mail.no"
            };

            User user2 = new User
            {
                Id = uuidUser2.ToString(),
                Email = "john@doe.no"
            };

            Comment comment_11 = new Comment(uuidComment1.ToString(), uuidDiscussion1.ToString(), "This is comment 11", user2, DateTime.Now);
            Comment comment_12 = new Comment(uuidComment2.ToString(), uuidDiscussion1.ToString(), "This is comment 12", user1, DateTime.Now);

            Comment comment_21 = new Comment(uuidComment3.ToString(), uuidDiscussion2.ToString(), "This is comment 21", user1, DateTime.Now);
            Comment comment_22 = new Comment(uuidComment4.ToString(), uuidDiscussion2.ToString(), "This is comment 22", user2, DateTime.Now);

            Discussion discussion1 = new Discussion(uuidDiscussion1.ToString(), "This is a header 1", "This is text 1", user1, DateTime.Now, DateTime.Now);
            Discussion discussion2 = new Discussion(uuidDiscussion2.ToString(), "This is a header 2", "This is text 2", user2, DateTime.Now, DateTime.Now);

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    user1,
                    user2,
                };
                context.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Discussions.Any())
            {
                var discussions = new List<Discussion>
                {
                    discussion1,
                    discussion2,
                };
                context.AddRange(discussions);
                context.SaveChanges();
            }

            if (!context.Comments.Any()) {
                var comments = new List<Comment>
                {
                    comment_11,
                    comment_12,
                    comment_21,
                    comment_22,
                };
                context.AddRange(comments);
                context.SaveChanges();
            }

            context.SaveChanges();
        }

	}
}

