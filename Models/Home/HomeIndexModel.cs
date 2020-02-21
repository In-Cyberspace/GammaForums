using System.Collections.Generic;
using GammaForums.Models.Post;

namespace GammaForums.Models.Home
{
    public class HomeIndexModel
    {
        /// <summary>
        // Latest posts across all forums
        /// </summary>
        public IEnumerable<PostListingModel> LatestPosts { get; set; }

        /// <summary>
        /// Search query input
        /// </summary>
        public string SearchQuery { get; set; }
    }
}