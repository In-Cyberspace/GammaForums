using System.Collections.Generic;
using Data;
using GammaForums.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GammaForums.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
        }

        public IActionResult Detail(string Id)
        {
            ApplicationUser user = _userService.GetById(Id);
            IList<string> userRoles = _userManager.GetRolesAsync(user).Result;

            return View(
                new ProfileModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRating = user.Rating.ToString(),
                    Email = user.Email,
                    ProfileImageUrl = user.ProfileImageUrl,
                    MemberSince = user.MemberSince,
                    IsAdmin = userRoles.Contains("Admin")
                }
            );
        }
    }
}