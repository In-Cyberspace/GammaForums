using System.Collections.Generic;
using GammaForums.Models.Post;

namespace GammaForums.Models.Search
{
    public class SearchResultModel
    {
        /// <summary>
        /// Collection of posts
        /// </summary>
        public IEnumerable<PostListingModel> Posts { get; set; }

        /// <summary>
        /// Search query input
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Boolean property to check if search results are empty
        /// </summary>
        public bool EmptySearchResults { get; set; }
    }
}