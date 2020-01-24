using System.Collections.Generic;
using GammaForums.Models.Post;

namespace GammaForums.Models.Home
{
    public class HomeIndexModel
    {
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
        public string SearchQuery { get; set; }
    }
}