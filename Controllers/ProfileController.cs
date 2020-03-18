using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data;
using GammaForums.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GammaForums.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public ProfileController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService)
        {
            _configuration = configuration;
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
            string userId = _userManager.GetUserId(User);

            // Connect to an Azure Storage Account Container.
            string connectionString =
            _configuration.GetConnectionString("AzureStorageAccount");

            // Get Blob Container.
            CloudBlobContainer container =
            _uploadService.GetBlobContainer(connectionString, "profile-images");

            // Parse the content disposition header on the http request.
            ContentDispositionHeaderValue contentDisposition =
            ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            // Grab the file namestring.
            string filename = contentDisposition.FileName.Trim('"');

            // Get a reference to a Block Blob.
            CloudBlockBlob blockBlob =
            container.GetBlockBlobReference(filename);

            // On that block blob, upload our file, using the file name <— file uploaded to cloud.
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            // Set the user’s profile image to the URI.
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            // Redirect to user’s profile page.
            return RedirectToAction("Detail", "Profile", new { Id = userId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IEnumerable<ProfileModel> profiles = _userService.GetAll()
            .OrderByDescending(user => user.Rating)
            .Select(u => new ProfileModel
            {
                Email = u.Email,
                UserName = u.UserName,
                ProfileImageUrl = u.ProfileImageUrl,
                UserRating = u.Rating.ToString(),
                MemberSince = u.MemberSince
            });

            return View(new ProfileListModel
            {
                Profiles = profiles
            });
        }
    }
}