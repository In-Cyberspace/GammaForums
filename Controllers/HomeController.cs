using System.Diagnostics;
using System.Linq;
using Data;
using GammaForums.Models;
using GammaForums.Models.Forum;
using GammaForums.Models.Home;
using GammaForums.Models.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GammaForums.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPost _postService;

        public HomeController(ILogger<HomeController> logger, IPost postService)
        {
            _logger = logger;
            _postService = postService;
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            Forum forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            return new HomeIndexModel
            {
                LatestPosts = _postService
                .GetLatestPosts(10)
                .Select(
                    post => new PostListingModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        AuthorId = post.User.Id,
                        AuthorName = post.User.UserName,
                        AuthorRating = post.User.Rating,
                        DatePosted = post.TimeCreated.ToString(),
                        RepliesCount = post.Replies.Count(),
                        Forum = GetForumListingForPost(post)
                    }
                ),

                SearchQuery = ""
            };
        }

        public IActionResult Index()
        {
            return View(BuildHomeIndexModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}