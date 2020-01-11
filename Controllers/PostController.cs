using System.Collections.Generic;
using System.Linq;
using Data;
using GammaForums.Models.Post;
using GammaForums.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        public PostController(IPost postService)
        {
            _postService = postService;
        }
        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies
            .Select(
                reply => new PostReplyModel
                {
                    Id = reply.Id,
                    AuthorID = reply.User.Id,
                    AuthorName = reply.User.UserName,
                    AuthorImageUrl = reply.User.ProfileImageUrl,
                    AuthorRating = reply.User.Rating,
                    TimeCreated = reply.TimeCreated,
                    ReplyContent = reply.Content
                }
            );
        }
        public IActionResult Index(int postId)
        {
            Post post = _postService.GetById(postId);

            return View(
                new PostIndexModel
                {
                    Id = post.Id,
                    AuthorID = post.User.Id,
                    AuthorName = post.User.UserName,
                    AuthorImageUrl = post.User.ProfileImageUrl,
                    AuthorRating = post.User.Rating,
                    TimeCreated = post.TimeCreated,
                    PostContent = post.Content,
                    Replies = BuildPostReplies(post.Replies)
                }
            );
        }

        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
    }
}