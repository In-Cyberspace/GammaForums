using System.Linq;
using Data;
using GammaForums.Models.Forum;
using GammaForums.Models.Post;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GammaForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            return BuildForumListing(post.Forum);
        }

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(
                new ForumIndexModel
                {
                    ForumList =
                    _forumService
                    .GetAll()
                    .Select(forum => new ForumListingModel
                    {
                        Id = forum.Id,
                        Title = forum.Title,
                        Description = forum.Description
                    })
                });
        }

        public IActionResult Topic(int id)
        {
            Forum forum = _forumService.GetById(id);

            return View(
                new ForumTopicModel
                {
                    Posts =
                    forum
                    .Posts
                    .Select(post => new PostListingModel
                    {
                        Id = post.Id,
                        AuthorId = post.User.Id,
                        AuthorRating = post.User.Rating,
                        Title = post.Title,
                        DatePosted = post.TimeCreated.ToString(),
                        RepliesCount = post.Replies.Count(),
                        Forum = BuildForumListing(post)
                    }),

                    Forum = BuildForumListing(forum)
                });
        }
    }
}