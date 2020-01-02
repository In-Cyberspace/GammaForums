using System.Linq;
using Data;
using GammaForums.Models.Forum;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GammaForums.Controllers {
    public class ForumController : Controller {
        private readonly IForum _forumService;

        public ForumController (IForum forumService) {
            _forumService = forumService;
        }

        // GET: /<controller>/
        public IActionResult Index () {
            var forums = _forumService
                .GetAll ()
                .Select (forum => new ForumListingModel {
                    Id = forum.Id,
                        Title = forum.Title,
                        Description = forum.Description
                });

            var model = new ForumIndexModel { ForumList = forums };

            return View (model);
        }
    }
}