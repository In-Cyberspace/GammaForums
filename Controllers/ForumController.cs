using System.Linq;
using Data;
using GammaForums.Models.Forum;
using GammaForums.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

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

        public ForumController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new ForumIndexModel
            {
                ForumList = _forumService
                .GetAll()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description
                })
            });
        }

        public IActionResult Topic(int Id, string searchQuery)
        {
            Forum forum = _forumService.GetById(Id);

            return View(new ForumTopicModel
            {
                Forum = BuildForumListing(forum),

                Posts = _postService
                .GetFilteredPosts(forum, searchQuery)
                .ToList()
                .Select(post => new PostListingModel
                {
                    Id = post.Id,
                    AuthorId = post.User.Id,
                    AuthorName = post.User.UserName,
                    AuthorRating = post.User.Rating,
                    Title = post.Title,
                    DatePosted = post.TimeCreated.ToString(),
                    RepliesCount = post.Replies.Count(),
                    Forum = BuildForumListing(post)
                })
            });
        }

        [HttpPost]
        public IActionResult Search(int Id, string searchQuery)
        {
            return RedirectToAction("Topic", new { Id, searchQuery });
        }
    }
}
