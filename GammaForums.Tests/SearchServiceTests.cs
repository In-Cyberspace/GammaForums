using System.Collections.Generic;
using System.Linq;
using Data;
using GammaForums.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GammaForums.Tests
{
    [TestFixture]
    public class Post_Service_Should
    {
        [Test]
        public void Return_Filtered_Results_Corresponding_To_Query()
        {
            DbContextOptions<ApplicationDbContext> options =
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Search_Database")
            .Options;

            // Arrange
            using (ApplicationDbContext ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 19
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 2290,
                    Title = "This Is A Post Title",
                    Content = "Quality."
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = -30,
                    Title = "Quality.",
                    Content = "Even higher quality post content."
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 590,
                    Title = "Bloatware",
                    Content = "Quality."
                });

                ctx.SaveChanges();
            }

            // Act
            using (ApplicationDbContext ctx = new ApplicationDbContext(options))
            {
                IPost postService = new PostService(ctx);
                IEnumerable<Post> result = postService.GetFilteredPosts("Coffee");
                int postCount = result.Count();

                // Assert
                Assert.AreEqual(3, postCount);
            }
        }
    }
}
