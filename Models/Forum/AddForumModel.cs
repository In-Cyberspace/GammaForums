using Microsoft.AspNetCore.Http;

namespace GammaForums.Models.Forum
{
    public class AddForumModel
    {
        /// <summary>
        /// The forum title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The forum description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Url pointing to the image associated with the forum.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Handles the process of uploading the forum image.
        /// </summary>
        public IFormFile ImageUpload { get; set; }
    }
}