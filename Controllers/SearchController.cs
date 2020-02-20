using System.Linq;
using GammaForums.Models.Forum;
using GammaForums.Models.Post;
using GammaForums.Models.Search;
using global::Data;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public ForumListingModel BuildForumListing(Post post)
        {
            Forum forum = post.Forum;

            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        public IActionResult Results(string searchQuery)
        {
            return View(new SearchResultModel
            {
                Posts = _postService.GetFilteredPosts(searchQuery)
                .Select(post
                    => new PostListingModel
                    {
                        Id = post.Id,
                        AuthorId = post.User.Id,
                        AuthorName = post.User.UserName,
                        AuthorRating = post.User.Rating,
                        Title = post.Title,
                        DatePosted = post.TimeCreated.ToString(),
                        RepliesCount = post.Replies.Count(),
                        Forum = BuildForumListing(post)
                    }
                )
            });
        }

        [HttpPost]
        public IActionResult Search(int ID, string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}