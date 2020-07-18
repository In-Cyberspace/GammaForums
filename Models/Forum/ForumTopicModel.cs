using System.Collections.Generic;
using GammaForums.Models.Post;

namespace GammaForums.Models.Forum
{
    public class ForumTopicModel
    {
        /// <summary>Provides basic information about the forum</summary>
        public ForumListingModel Forum { get; set; }

        /// <summary>List of posts related to the forum</summary>
        public IEnumerable<PostListingModel> Posts { get; set; }

        /// <summary>Search within the forum</summary>
        public string SearchQuery { get; set; }

        public bool EmptySearchResults { get; set; }
    }
}