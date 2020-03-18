using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data;
using GammaForums.Models.Forum;
using GammaForums.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GammaForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly IUpload _uploadService;

        public ForumController(
            IConfiguration configuration,
            IForum forumService,
            IPost postService,
            IUpload uploadService)
        {
            _configuration = configuration;
            _forumService = forumService;
            _postService = postService;
            _uploadService = uploadService;

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
                    Description = forum.Description,
                    NumberOfPosts = forum.Posts?.Count() ?? 0,
                    NumberOfUsers = _forumService.GetActiveUsers(forum.Id).Count(),
                    ImageUrl = forum.ImageUrl,
                    HasRecentPost = _forumService.HasRecentPost(forum.Id)
                })
                .OrderBy(f => f.Title)
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

        public IActionResult Create()
        {
            AddForumModel model = new AddForumModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {
            string imageUri = "/images/default/default.png";

            if (model.ImageUpload != null)
            {
                CloudBlockBlob blockBlob = UploadForumImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            Forum forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                TimeCreated = DateTime.Now,
                ImageUrl = imageUri
            };

            await _forumService.Create(forum);

            return RedirectToAction("Index", "Forum");
        }

        private CloudBlockBlob UploadForumImage(IFormFile file)
        {
            string connectionString =
            _configuration.GetConnectionString("AzureStorageAccount");

            CloudBlobContainer container =
            _uploadService.GetBlobContainer(connectionString, "forum-images");

            ContentDispositionHeaderValue contentDisposition =
            ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            string filename = contentDisposition.FileName.Trim('"');

            CloudBlockBlob blockBlob =
            container.GetBlockBlobReference(filename);

            blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob;
        }

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
    }
}
