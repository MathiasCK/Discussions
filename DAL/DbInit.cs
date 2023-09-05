using Discussions.Models;

namespace Discussions.DAL
{
	public class DbInit
	{

        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            DB context = serviceScope.ServiceProvider.GetRequiredService<DB>();

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine("ER22" + e.Message);
            }
            


            User user1 = new User
            {
                Id = 1,
                Email = "mck@mail.no"
            };

            User user2 = new User
            {
                Id = 2,
                Email = "john@doe.no"
            };

            Comment comment_11 = new Comment(11, 1, "This is comment 11", user2, DateTime.Now);
            Comment comment_12 = new Comment(12, 1, "This is comment 12", user1, DateTime.Now);

            Comment comment_21 = new Comment(21, 2, "This is comment 21", user1, DateTime.Now);
            Comment comment_22 = new Comment(22, 2, "This is comment 22", user2, DateTime.Now);

            Discussion discussion1 = new Discussion(1, "This is a header 1", "This is text 1", user1, DateTime.Now, DateTime.Now);
            Discussion discussion2 = new Discussion(2, "This is a header 2", "This is text 2", user2, DateTime.Now, DateTime.Now);

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

