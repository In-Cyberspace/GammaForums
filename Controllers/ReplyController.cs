using System;
using System.Threading.Tasks;
using Data;
using GammaForums.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPost _postService;
        private readonly IApplicationUser _userService;

        public ReplyController(
                UserManager<ApplicationUser> userManager,
                IPost postService,
                IApplicationUser userService)

        {
            _userManager = userManager;
            _postService = postService;
            _userService = userService;
        }

        public async Task<IActionResult> Create(int Id)
        {
            Post post = _postService.GetById(Id);

            ApplicationUser user =
            await _userManager.FindByNameAsync(User.Identity.Name);

            return View(new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,
                AuthorId = user.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),
                ForumId = post.Forum.Id,
                ForumTitle = post.Forum.Title,
                ForumImageUrl = post.Forum.ImageUrl,
                TimeCreated = DateTime.Now
            });
        }

        private PostReply BuildReply(
            PostReplyModel model,
            ApplicationUser user)
        {

            Post post = _postService.GetById(model.PostId);

            return new PostReply
            {
                Post = post,
                Content = model.ReplyContent,
                TimeCreated = DateTime.Now,
                User = user
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            string userId = _userManager.GetUserId(User);

            ApplicationUser user =
            await _userManager.FindByIdAsync(userId);

            PostReply reply = BuildReply(model, user);

            await _postService.AddReply(reply);
            await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { Id = model.PostId });
        }
    }
}
