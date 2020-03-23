using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using GammaForums.Models.Post;
using GammaForums.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    public class PostController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public PostController(
            IForum forumService,
            IPost postService,
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService)
        {
            _forumService = forumService;
            _postService = postService;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize]
        public IActionResult Create(int Id)
        {
            Forum forum = _forumService.GetById(Id);

            return View(
                new NewPostModel
                {
                    ForumTitle = forum.Title,
                    ForumId = forum.Id,
                    ForumImageUrl = forum.ImageUrl,
                    AuthorName = User.Identity.Name
                }
            );
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            string userId = _userManager.GetUserId(User);

            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

            Post post = BuildPost(model, user);

            await _postService.Add(post);
            await _userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Post", new { Id = post.Id });
        }

        public IActionResult Index(int Id)
        {
            Post post = _postService.GetById(Id);

            return View(
                new PostIndexModel
                {
                    Id = post.Id,
                    AuthorId = post.User.Id,
                    AuthorName = post.User.UserName,
                    AuthorImageUrl = post.User.ProfileImageUrl,
                    AuthorRating = post.User.Rating,
                    TimeCreated = post.TimeCreated,
                    Title = post.Title,
                    PostContent = post.Content,
                    Replies = BuildPostReplies(post.Replies),
                    ForumId = post.Forum.Id,
                    ForumName = post.Forum.Title,
                    IsAuthorAdmin = IsAuthorAdmin(post.User)
                }
            );
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");
        }


        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                TimeCreated = DateTime.Now,
                User = user,
                Forum = _forumService.GetById(model.ForumId)
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(
            IEnumerable<PostReply> replies)
        {
            return replies
                .Select(
                    reply => new PostReplyModel
                    {
                        Id = reply.Id,
                        AuthorId = reply.User.Id,
                        AuthorName = reply.User.UserName,
                        AuthorImageUrl = reply.User.ProfileImageUrl,
                        AuthorRating = reply.User.Rating,
                        TimeCreated = reply.TimeCreated,
                        ReplyContent = reply.Content,
                        IsAuthorAdmin = IsAuthorAdmin(reply.User)
                    }
                );
        }
    }
}
