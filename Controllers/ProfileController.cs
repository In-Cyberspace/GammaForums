using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using GammaForums.Models.ApplicationUser;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file is null)
            {
                throw new System.ArgumentNullException(nameof(file));
            }

            string userId = _userManager.GetUserId(User);
            // Connect to an Azure Storage Account Container.
            // Get Blob Container.

            // Parse the content disposition header on the http request.
            // Grab the file name

            // Get a reference to a Block Blob (a particular type of blob that is going to be uploaded to our blob container, which we have an API for so it’s not difficult)
            // On that block blob, upload our file, using the file name <— file uploaded to cloud

            // Set the user’s profile image to the URI
            // Redirect to user’s profile page
        }
    }
}